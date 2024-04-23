using TV2.Backend.Services.VideoAnalyser.Client;
using TV2.Backend.Services.VideoAnalyser.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IAnalyserService, AnalyserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


/*
using System.Text.Json;

namespace TV2.Backend.Services.VideoAnalyserService
{
    using Client;
    using Client.Model;
    public class Program
    {
        //Choose public Access Video URL or File Path
        private const string VideoUrl = "https://tv2footage.blob.core.windows.net/test-container/CodingPirates%20TV2%20Nyhederne.mp4?sp=racwdyi&st=2024-04-10T12:01:44Z&se=2024-07-31T20:01:44Z&spr=https&sv=2022-11-02&sr=b&sig=Rjhf%2BlqwLQg%2FZ31b%2FIRwJw%2B5NdVf97KQJXNY1Z8FWuE%3D";
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
            
            
            // Create Video Indexer Clients
            var authService = new AuthService();
            var analyserService = new AnalyserService();
            // Get Access Tokens
            var armToken = await authService.AuthenticateArmAsync();
            var token = await authService.AuthenticateAsync(armToken);
            
            // Sample 1 : Get account details, not required in most cases
            Console.WriteLine("Sample 1 - Get Account Basic Details");
            var account = await authService.GetAccountAsync(Consts.ViAccountName, armToken);
            Console.WriteLine($"Account ID {account.Properties.Id} was successfully retrieved");
            
            
            // Sample 2 :  Upload a video , do not wait for the index operation to complete. 
            Console.WriteLine("Sample 2 - Index a Video from URL");
            var videoId = await analyserService.UploadUrlAsync(VideoUrl, "video-1", account, token, ExcludedAI, false);
            Console.WriteLine($"Video ID {videoId} was successfully uploaded");
            

            var videoId = "89b32d3c1d";
            // Sample 3 : Wait for the video index to finish ( Polling method)
            Console.WriteLine("Sample 3 - Polling on Video Completion Event");
            var videoGetIndexResult = await analyserService.WaitForIndexAsync(videoId, account, token);
            //Console.WriteLine($"The video index has completed. Here is the full JSON of the index for video ID {videoId}: \n{videoGetIndexResult}");
            var index = JsonSerializer.Deserialize<Index>(videoGetIndexResult);
            foreach (var topic in index.Videos[0].Metadata.Topics)
            {
                Console.WriteLine(topic.Name);
            }
            foreach (var keyword in index.Videos[0].Metadata.Keywords)
            {
                Console.WriteLine(keyword.Text);
            }
            Console.WriteLine(JsonSerializer.Serialize(index.Videos[0].Metadata));

            
            // Sample 4: Search for the video and get insights
            Console.WriteLine("Sample 4 - Search for Video And get insights");
            var searchResult = await analyserService.GetVideoAsync(videoId, account, token);
            Console.WriteLine($"Here are the search results: \n{searchResult}");

            
            // TODO: Doesn't work, responds with "401 Unauthorized"
            // Sample 5: Widgets API's
            Console.WriteLine("Sample 5- Widgets API");
            
            var insightsWidgetLink = await analyserService.GetInsightsWidgetUrlAsync(videoId, account, armToken);
            Console.WriteLine($"Got the insights widget URL: \n{insightsWidgetLink}");
            
            var playerWidgetLink = await analyserService.GetPlayerWidgetUrlAsync(videoId, account, armToken);
            Console.WriteLine($"Got the player widget URL: \n{playerWidgetLink}");
        }

    }
}
*/
