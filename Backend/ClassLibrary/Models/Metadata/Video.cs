using System.Text.Json.Serialization;

namespace TV2.Backend.ClassLibrary.Models.Metadata;

public class Video
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    
    [JsonPropertyName("publishedUrl")]
    public string? PublishedUrl { get; set; }
    
    [JsonPropertyName("insights")]
    public Metadata? Metadata { get; set; }
}