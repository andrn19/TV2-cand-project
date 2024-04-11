namespace TV2.Backend.ClassLibrary.Models.Metadata;

using System.Text.Json.Serialization;

public class Metadata
{
    [JsonPropertyName("keywords")]
    public List<Keyword>? Keywords { get; set; }

    /*
    [JsonPropertyName("topics")]
    public List<string>? Topics { get; set; }
    */
}