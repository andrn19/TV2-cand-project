using System.Text.Json.Serialization;

namespace TV2.Backend.ClassLibrary.Models.Metadata;


public class Metadata
{
    [JsonPropertyName("faces")]
    public List<Face>? Faces { get; set; }
    
    [JsonPropertyName("topics")]
    public List<Topic>? Topics { get; set; }
    
    [JsonPropertyName("labels")]
    public List<Label>? Labels { get; set; }
    
    [JsonPropertyName("keywords")]
    public List<Keyword>? Keywords { get; set; }
    
    [JsonPropertyName("namedPeople")]
    public List<NamedPerson>? NamedPeople { get; set; }
    
    [JsonPropertyName("namedLocations")]
    public List<NamedLocation>? NamedLocations { get; set; }
    
    [JsonPropertyName("shots")]
    public List<Shot>? Shots { get; set; }
    
    [JsonPropertyName("transcript")]
    public List<Transcript>? Transcripts { get; set; }
}