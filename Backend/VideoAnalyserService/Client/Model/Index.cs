
namespace TV2.Backend.Services.VideoAnalyser.Client.Model;

using ClassLibrary.Models.Metadata;
using System.Text.Json.Serialization;

public class Index
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("videos")]
    public List<Video>? Videos { get; set; }
}