using System.Text.Json.Serialization;

namespace VideoAnalyser.Client.Auth
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ArmAccessTokenScope
    {
        Account,
        Project,
        Video
    }
}