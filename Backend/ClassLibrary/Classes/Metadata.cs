namespace TV2.Backend.ClassLibrary.Classes;

public class Metadata
{
    public Metadata(Guid id, string title, string description)
    {
        Id = id;
        Title = title;
        Description = description;
    }

    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    
}