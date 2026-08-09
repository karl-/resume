using HandlebarsDotNet;
using Markdig;

namespace Resume;

static class Program
{
    static readonly string[] SourceFiles =
    [
        "Contact.md",
        "Experience.md",
        "Education.md",
        "Skills.md",
        "Presentations.md"
    ];

    const string DefaultTemplatePath = "src/Templates/Standard.html";
    const string DefaultOutputPath = "Resume.html";

    static void Main(string[] args)
    {
        var templatePath = args?.IndexOf("--template") is { } templateIndex
                           && templateIndex > -1
                           && templateIndex < args.Length - 1
                           && File.Exists(args[templateIndex + 1])
            ? args[templateIndex + 1]
            : DefaultTemplatePath;

        var outputPath = args?.IndexOf("--output") is {} outputIndex
                         && outputIndex > -1
                         && outputIndex < args.Length - 1
            ? args[outputIndex + 1]
            : DefaultOutputPath;

        var source = File.ReadAllText(templatePath);
        var template = Handlebars.Compile(source);
        
        var data = SourceFiles.ToDictionary(
            file => Path.GetFileNameWithoutExtension(file)!,
            file => Markdown.ToHtml(File.ReadAllText(file).Trim()));

        var stylesheetPath = Path.ChangeExtension(templatePath, ".css");
        data["Css"] = File.Exists(stylesheetPath)
            ? File.ReadAllText(stylesheetPath).Trim()
            : string.Empty;

        File.WriteAllText(outputPath, template(data).TrimEnd() + Environment.NewLine);
        Console.WriteLine($"Generated {outputPath}");
    }
}
