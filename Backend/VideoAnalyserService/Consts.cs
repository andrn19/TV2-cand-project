namespace TV2.Backend.Services.VideoAnalyser;

public static class Consts
{
    public const string ApiVersion = "2022-08-01";
    public const string AzureResourceManager = "https://management.azure.com";
    public static readonly string SubscriptionId = "a269438d-cc34-44bc-979a-260e9eacaf11";
    public static readonly string ResourceGroup = "New-Resource-Group";
    public static readonly string ViAccountName = "TV2-Video-Analyser-2";
    public static readonly string ApiEndpoint = "https://api.videoindexer.ai"; //Environment.GetEnvironmentVariable("API_ENDPOINT") ?? 
    
    public static bool Valid() => !string.IsNullOrWhiteSpace(SubscriptionId) && 
                                  !string.IsNullOrWhiteSpace(ResourceGroup) &&
                                  !string.IsNullOrWhiteSpace(ViAccountName);
}