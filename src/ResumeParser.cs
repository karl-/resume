using System.Text.Json;

namespace Resume;

public static class ResumeParser
{
    const string FilePrefix = "file://";

    static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static Resume Parse(string path)
    {
        var manifestPath = Path.GetFullPath(path);
        var manifestDirectory = Path.GetDirectoryName(manifestPath) ?? Directory.GetCurrentDirectory();
        var manifest = Deserialize<ResumeManifest>(manifestPath);
        
        var experience = ReadSection<ExperienceSection>(manifest.Experience, manifestDirectory).Experience;

        foreach (var item in experience)
            item.Description = ParseTextOrPathField(item.Description, manifestDirectory);

        return new Resume
        {
            Contact = ReadSection<Contact>(manifest.Contact, manifestDirectory),
            Summary = ParseTextOrPathField(manifest.Summary, manifestDirectory),
            Experience = experience,
            Education = ReadOptionalSection<Education>(manifest.Education, manifestDirectory) is {} education ? [education] : [],
            Skills = ReadOptionalSection<SkillsSection>(manifest.Skills, manifestDirectory) is {} skills ? skills.Skills : [],
            Presentations = ReadSection<PresentationSection>(manifest.Presentations, manifestDirectory).Presentations
        };
    }

    static T ReadSection<T>(string reference, string baseDirectory)
    {
        if (string.IsNullOrWhiteSpace(reference))
            throw new InvalidDataException($"Missing reference for {typeof(T).Name}.");

        return Deserialize<T>(ResolveReference(reference, baseDirectory));
    }

    static T? ReadOptionalSection<T>(string? reference, string baseDirectory) where T : class =>
        string.IsNullOrWhiteSpace(reference) ? null : ReadSection<T>(reference, baseDirectory);

    static T Deserialize<T>(string path)
    {
        using var stream = File.OpenRead(path);
        return JsonSerializer.Deserialize<T>(stream, JsonOptions)
            ?? throw new InvalidDataException($"Could not deserialize '{path}' as {typeof(T).Name}.");
    }

    static string ResolveReference(string reference, string baseDirectory)
    {
        var relativePath = reference.StartsWith(FilePrefix, StringComparison.OrdinalIgnoreCase)
            ? reference[FilePrefix.Length..]
            : reference;

        return Path.GetFullPath(relativePath, baseDirectory);
    }

    static string ParseTextOrPathField(string value, string baseDirectory) =>
        value.StartsWith(FilePrefix, StringComparison.OrdinalIgnoreCase)
            ? File.ReadAllText(ResolveReference(value, baseDirectory)).Trim()
            : value;

    sealed class ResumeManifest
    {
        public string Summary { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public string? Education { get; set; }
        public string Skills { get; set; } = string.Empty;
        public string Presentations { get; set; } = string.Empty;
    }

    sealed class ExperienceSection
    {
        public List<Experience> Experience { get; set; } = [];
    }

    sealed class SkillsSection
    {
        public List<string> Skills { get; set; } = [];
    }

    sealed class PresentationSection
    {
        public List<Presentation> Presentations { get; set; } = [];
    }
}
