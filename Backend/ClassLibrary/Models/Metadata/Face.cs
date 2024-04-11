using System.Text.Json.Serialization;

namespace TV2.Backend.ClassLibrary.Models.Metadata;

public class Face
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("confidence")]
    public int Confidence { get; set; }
    
    [JsonPropertyName("thumbnailId")]
    public Guid? Thumbnail { get; set; }
    
    [JsonPropertyName("instances")]
    public List<Instance>? Instances { get; set; }
}