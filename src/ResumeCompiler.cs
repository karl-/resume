using HandlebarsDotNet;
using Markdig;

namespace Resume;

static class Program
{
    const string DefaultContentPath = "content/Resume.json";
    const string DefaultTemplatePath = "src/Templates/Standard.html";
    const string DefaultOutputPath = "bin/Resume.html";

    static void Main(string[] args)
    {
        var templatePath = GetArg(args, "--template", DefaultTemplatePath);
        var outputPath = GetArg(args, "--output", DefaultOutputPath);
        var contentPath = GetArg(args, "--content", DefaultContentPath);

        var source = File.ReadAllText(templatePath);

        Handlebars.RegisterHelper("markdown", (writer, context, parameters) =>
        {
            var markdown = parameters.Length > 0 ? parameters[0]?.ToString() : string.Empty;
            writer.WriteSafeString(Markdown.ToHtml(markdown ?? string.Empty).Trim());
        });
        
        Handlebars.RegisterHelper("markdownInline", (writer, context, parameters) =>
        {
            var markdown = parameters.Length > 0 ? parameters[0]?.ToString() : string.Empty;
            var html = Markdown.ToHtml(markdown ?? string.Empty).Trim();
            if (html.StartsWith("<p>", StringComparison.Ordinal) &&
                html.EndsWith("</p>", StringComparison.Ordinal))
                html = html[3..^4];

            writer.WriteSafeString(html);
        });

        var template = Handlebars.Compile(source);

        var stylesheetPath = Path.ChangeExtension(templatePath, ".css");
        var css = File.Exists(stylesheetPath)
            ? File.ReadAllText(stylesheetPath).Trim()
            : string.Empty;
        var resume = ResumeParser.Parse(contentPath);
        var data = new
        {
            resume.Contact,
            resume.Experience,
            resume.Education,
            resume.Skills,
            resume.Presentations,
            Css = css
        };

        Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
        File.WriteAllText(outputPath, template(data).TrimEnd() + Environment.NewLine);
        Console.WriteLine($"Generated {outputPath}");
    }
    
    static string GetArg(string[] args, string arg, string defaultValue)
    {
         return args?.IndexOf(arg) is { } templateIndex
                           && templateIndex > -1
                           && templateIndex < args.Length - 1
                           && File.Exists(args[templateIndex + 1])
            ? args[templateIndex + 1]
            : defaultValue;
    }
}
