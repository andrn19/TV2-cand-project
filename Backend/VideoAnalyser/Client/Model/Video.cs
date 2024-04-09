namespace TV2.Backend.Services.VideoAnalyser.Client.Model;

using System.Text.Json.Serialization;

public class Video
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }
}