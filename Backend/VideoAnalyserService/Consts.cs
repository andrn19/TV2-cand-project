namespace TV2.Backend.Services.VideoAnalyser;

public static class Consts
{
    public const string ApiVersion = "2022-08-01";
    public const string AzureResourceManager = "https://management.azure.com";
    public static readonly string SubscriptionId = "f63bd84a-b56c-4dbe-8f82-1a28bb4a7640";
    public static readonly string ResourceGroup = "TV2-Video-Analyser";
    public static readonly string ViAccountName = "Metadata-Generator";
    public static readonly string ApiEndpoint = Environment.GetEnvironmentVariable("API_ENDPOINT") ?? "https://api.videoindexer.ai";

    //public static bool Valid() => true;
    public static bool Valid() => !string.IsNullOrWhiteSpace(SubscriptionId) && 
                                  !string.IsNullOrWhiteSpace(ResourceGroup) &&
                                  !string.IsNullOrWhiteSpace(ViAccountName);
}