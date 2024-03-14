namespace TV2.Backend.ClassLibrary.Classes;

public class MetadataHost
{
    public MetadataHost(string url, string port, string name)
    {
        Url = url;
        Port = port;
        Name = name;
    }

    public string Name { get; set; }
    public string Url { get; set; }
    public string Port { get; set; }
}