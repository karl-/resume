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
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string Phone { get; set; }
    public required string Website { get; set; }
}

public class Experience
{
    public required string Location { get; set; }
    public required string Title { get; set; }
    public required string Date { get; set; }
    public required string Contact { get; set; }
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