namespace TV2.Backend.Services.VideoAnalyser.Client.Model;

using System.Text.Json.Serialization;

public class AccountProperties
{
    [JsonPropertyName("accountId")]
    public string? Id { get; set; }
}