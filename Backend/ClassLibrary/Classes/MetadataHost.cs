namespace TV2.ClassLibrary.Classes;

public class MetadataHost
{
    public MetadataHost(MetadataHostIdentifier identifier, string url, string port, string name)
    {
        Id = identifier;
        Url = url;
        Port = port;
    }

    public MetadataHostIdentifier Id { get; set; }
    public string Url { get; set; }
    public string Port { get; set; }
}