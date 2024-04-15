using System.Text.Json.Serialization;

namespace TV2.Backend.ClassLibrary.Models.Metadata;

public class Keyframe
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("instances")]
    public List<KeyframeInstance>? Instances { get; set; }
}