namespace Resume;

public class Resume
{
    public Contact @Contact;
    public List<Experience> Experience = [];
    public List<Education> Education = [];
    public List<string> Skills = [];
}

public class Contact
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Website { get; set; }
}

public class Experience
{
    public string Location { get; set; }
    public string Title { get; set; }
    public string Date { get; set; }
    public string Contact { get; set; }
    public string Description { get; set; }
}

public class Education
{
    public string School { get; set; } 
    public string Location { get; set; }
    public string Degree { get; set; }
    public string Date { get; set; }
    public List<string> Awards { get; set; }
}

