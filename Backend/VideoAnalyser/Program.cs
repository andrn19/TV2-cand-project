using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace VideoAnalyser
{
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

    
    public class Program
    {
        //Choose public Access Video URL or File Path
        private const string VideoUrl = "https://tv2footage.blob.core.windows.net/test-container/CodingPirates%20TV2%20Nyhederne.mp4?sp=racwdyi&st=2024-04-08T13:25:18Z&se=2024-04-08T21:25:18Z&spr=https&sv=2022-11-02&sr=b&sig=koOBVTdRbQNX6kRtPKKI36EKpdOMUtaWqisdZ%2BzS5E4%3D";
        //OR 
        private const string LocalVideoPath = "<Your Video File Path Here>";
        
        // Enter a list seperated by a comma of the AIs you would like to exclude in the format "<Faces,Labels,Emotions,ObservedPeople>". Leave empty if you do not want to exclude any AIs. For more see here https://api-portal.videoindexer.ai/api-details#api=Operations&operation=Upload-Video:~:text=AI%20to%20exclude%20when%20indexing%2C%20for%20example%20for%20sensitive%20scenarios.%20Options%20are%3A%20Face/Observed%20peopleEmotions/Labels%7D.
        private const string ExcludedAI = ""; 

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Video Indexer API Samples ");
            Console.WriteLine("=========================== ");

            if (!Consts.Valid())
            {
                throw new Exception(
                    "Please Fill In SubscriptionId, Account Name and Resource Group on the Constant Class !");
            }
            
            
            Console.WriteLine("Create client");
            // Create Video Indexer Client
            var client = new Client.Client();
            //Get Access Tokens
            Console.WriteLine("Authenticate");
            await client.AuthenticateAsync();

            //1. Sample 1 : Get account details, not required in most cases
            Console.WriteLine("Sample 1 - Get Account Basic Details");
            await client.GetAccountAsync(Consts.ViAccountName);
            
            //2. Sample 2 :  Upload a video , do not wait for the index operation to complete. 
            Console.WriteLine("Sample 2 - Index a Video from URL");
            var videoId = await client.UploadUrlAsync(VideoUrl, "my-video-name", ExcludedAI, false);
            
            //2A. Sample 2A : Upload From Local File 
            if (File.Exists(LocalVideoPath))
            {
                Console.WriteLine("Sample 2A - Index a video From File");
                var fileVideoId = await client.FileUploadAsync("my-other-video-name", LocalVideoPath);
            }

            // Sample 3 : Wait for the video index to finish ( Polling method)
            Console.WriteLine("Sample 3 - Polling on Video Completion Event");
            await client.WaitForIndexAsync(videoId);

            // Sample 4: Search for the video and get insights
            Console.WriteLine("Sample 4 - Search for Video And get insights");
            await client.GetVideoAsync(videoId);

            // Sample 5: Widgets API's
            Console.WriteLine("Sample 5- Widgets API");
            await client.GetInsightsWidgetUrlAsync(videoId);
            await client.GetPlayerWidgetUrlAsync(videoId);
            


            Console.WriteLine("\nPress Enter to exit...");
            var line = Console.ReadLine();
            if (line == "enter")
            {
                System.Environment.Exit(0);
            }
        }

    }
}