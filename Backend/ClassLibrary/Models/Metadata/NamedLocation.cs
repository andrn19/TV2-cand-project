using System.Text.Json.Serialization;

namespace TV2.Backend.ClassLibrary.Models.Metadata;

public class NamedLocation
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }
    
    [JsonPropertyName("confidence")]
    public double Confidence { get; set; }
    
    [JsonPropertyName("instances")]
    public List<Instance>? Instances { get; set; }
}