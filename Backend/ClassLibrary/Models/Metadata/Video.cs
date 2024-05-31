using System.Text.Json.Serialization;

namespace TV2.Backend.ClassLibrary.Models.Metadata;

public class Video
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("publishedUrl")]
    public string? PublishedUrl { get; set; }
    
    [JsonPropertyName("insights")]
    public Metadata? Metadata { get; set; }
    
    [JsonPropertyName("processingProgress")]
    public string? Progress { get; set; }
}