namespace TV2.Backend.Services.VideoAnalyser.Client.Auth
{
    using System.Text.Json.Serialization;
    
    public class GenerateAccessTokenResponse
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
    }
}