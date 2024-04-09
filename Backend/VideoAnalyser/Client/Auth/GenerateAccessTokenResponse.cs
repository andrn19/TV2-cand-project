using System.Text.Json.Serialization;

namespace VideoAnalyser.Client.Auth
{
    public class GenerateAccessTokenResponse
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
    }
}