namespace TV2.Backend.ClassLibrary.Classes;

public class MetadataHost
{
    public MetadataHost(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
}