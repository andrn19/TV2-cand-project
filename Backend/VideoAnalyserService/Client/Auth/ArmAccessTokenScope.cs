namespace TV2.Backend.Services.VideoAnalyser.Client.Auth
{
    using System.Text.Json.Serialization;
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ArmAccessTokenScope
    {
        Account,
        Project,
        Video
    }
}