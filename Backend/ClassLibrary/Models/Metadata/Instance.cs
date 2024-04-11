using System.Text.Json.Serialization;

namespace TV2.Backend.ClassLibrary.Models.Metadata;

public class Instance
{
    [JsonPropertyName("start")]
    public string? Start { get; set; }
    
    [JsonPropertyName("end")]
    public string? End { get; set; }
}

public class KeyframeInstance : Instance
{
    [JsonPropertyName("thumbnailId")]
    public Guid? ThumbnailId { get; set; }
}