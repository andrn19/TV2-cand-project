using System.Text.Json.Serialization;

namespace VideoAnalyser.Client.Model;

public class AccountProperties
{
    [JsonPropertyName("accountId")]
    public string Id { get; set; }
}