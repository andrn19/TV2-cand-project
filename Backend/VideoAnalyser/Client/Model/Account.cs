namespace TV2.Backend.Services.VideoAnalyser.Client.Model;

using System.Text.Json.Serialization;

public class Account
{
    [JsonPropertyName("properties")]
    public AccountProperties? Properties { get; set; }

    [JsonPropertyName("location")]
    public string? Location { get; set; }
}