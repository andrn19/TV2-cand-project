namespace TV2.ClassLibrary.Classes;

public class MetadataHostIdentifier
{
    public MetadataHostIdentifier(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
}