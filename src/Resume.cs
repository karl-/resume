namespace Resume;

public class Resume
{
    public required Contact @Contact;
    public List<Experience> Experience = [];
    public List<Education> Education = [];
    public List<string> Skills = [];
    public List<Presentation> Presentations = [];
}

public class Contact
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
}

public class Experience
{
    public string Employer { get; set; } = string.Empty;
    public required string Location { get; set; }
    public required string Title { get; set; }
    public required string Date { get; set; }
    public string Contact { get; set; } = string.Empty;
    public required string Description { get; set; }
}

public class Education
{
    public required string School { get; set; } 
    public required string Location { get; set; }
    public required string Degree { get; set; }
    public required string Date { get; set; }
    public List<string> Awards { get; set; } = [];
}

public class Presentation
{
    public required string Title { get; set; }
    public required string Conference { get; set; }
    public required string Location { get; set; }
    public required string Date { get; set; }
}
