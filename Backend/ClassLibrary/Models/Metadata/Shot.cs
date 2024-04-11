using System.Text.Json.Serialization;

namespace TV2.Backend.ClassLibrary.Models.Metadata;

public class Shot
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("keyFrames")]
    public List<Keyframe>? Keyframes { get; set; }
    
    [JsonPropertyName("instances")]
    public List<Shot>? Shots { get; set; }
    
    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }
}