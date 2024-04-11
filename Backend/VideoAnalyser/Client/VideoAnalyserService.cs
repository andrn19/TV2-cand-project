namespace TV2.Backend.Services.VideoAnalyser.Client;

using System.Text.Json;
using Model;
using Utils;
using Interfaces;
using static Consts;

public class VideoAnalyserService : IVideoAnalyserService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<VideoAnalyserService>? _logger;
    private readonly TimeSpan _pollingInterval = TimeSpan.FromSeconds(10);
    private string schema = "faces,topics,labels,keywords,namedLocations,namedPeople,shots,transcript";

    public VideoAnalyserService(ILogger<VideoAnalyserService>? logger = null)
    {
        System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;
        _httpClient = HttpClientUtils.CreateHttpClient();
        _logger = logger;
    }
    
    public async Task<string> UploadUrlAsync(string videoUrl, string videoName, Account account,
        string accountAccessToken, string exludedAIs = null, bool waitForIndex = false)
    {
        try
        {
            //Build Query Parameter Dictionary
            var queryDictionary = new Dictionary<string, string>
            {
                {"name", videoName},
                {"description", "video_description"},
                {"privacy", "private"},
                {"accessToken", accountAccessToken},
                {"videoUrl", videoUrl}
            };

            if (!Uri.IsWellFormedUriString(videoUrl, UriKind.Absolute))
            {
                throw new ArgumentException("VideoUrl or LocalVidePath are invalid");
            }

            var queryParams = queryDictionary.CreateQueryString();
            if (!string.IsNullOrEmpty(exludedAIs))
                queryParams += AddExcludedAIs(exludedAIs);

            // Send POST request
            var url = $"{ApiEndpoint}/{account.Location}/Accounts/{account.Properties.Id}/Videos?{queryParams}";
            var uploadRequestResult = await _httpClient.PostAsync(url, null);
            uploadRequestResult.VerifyStatus(System.Net.HttpStatusCode.OK);
            var uploadResult = await uploadRequestResult.Content.ReadAsStringAsync();

            // Get the video ID from the upload result
            var videoId = JsonSerializer.Deserialize<Index>(uploadResult).Id;

            if (waitForIndex)
            {
                _logger.LogInformation("Waiting for Index Operation to Complete");
                await WaitForIndexAsync(videoId, account, accountAccessToken);
            }

            return videoId;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    
    public async Task<string> WaitForIndexAsync(string videoId, Account account, string accountAccessToken)
    {
        while (true)
        {
            var queryParams = new Dictionary<string, string>()
            {
                {"language", "English"},
                {"includedInsights", schema},
                {"includeSummarizedInsights", "false"},
                {"accessToken", accountAccessToken}
            }.CreateQueryString();

            var requestUrl = $"{ApiEndpoint}/{account.Location}/Accounts/{account.Properties.Id}/Videos/{videoId}/Index?{queryParams}";
            var videoGetIndexRequestResult = await _httpClient.GetAsync(requestUrl);
            videoGetIndexRequestResult.VerifyStatus(System.Net.HttpStatusCode.OK);
            var videoGetIndexResult = await videoGetIndexRequestResult.Content.ReadAsStringAsync();
            string processingState = JsonSerializer.Deserialize<Index>(videoGetIndexResult).State;

            // If job is finished
            if (processingState == ProcessingState.Processed.ToString())
            {
                return videoGetIndexResult;
            }
            if (processingState == ProcessingState.Failed.ToString())
            {
                //Console.WriteLine($"The video index failed for video ID {videoId}.");
                throw new Exception(videoGetIndexResult);
            }

            // Job hasn't finished
            Console.WriteLine($"The video index state is {processingState}");
            await Task.Delay(_pollingInterval);
        }
    }

    
    public async Task<string> GetVideoAsync(string videoId, Account account, string accountAccessToken)
    {
        var queryParams = new Dictionary<string, string>()
        {
            { "accessToken" , accountAccessToken },
            { "id" , videoId },
        }.CreateQueryString();

        try
        {
            var requestUrl = $"{ApiEndpoint}/{account.Location}/Accounts/{account.Properties.Id}/Videos/Search?{queryParams}";
            var searchRequestResult = await _httpClient.GetAsync(requestUrl);
            searchRequestResult.VerifyStatus(System.Net.HttpStatusCode.OK);
            var searchResult = await searchRequestResult.Content.ReadAsStringAsync();
            return searchResult;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return "";
    }

    
    public async Task<Uri?> GetInsightsWidgetUrlAsync(string videoId, Account account, string accountAccessToken)
    {
        var queryParams = new Dictionary<string, string>()
        {
            { "widgetType" , "Keywords" },
            { "allowEdit" , "true" },
            { "accessToken" , accountAccessToken }
        }.CreateQueryString();
        try
        {
            var requestUrl = $"{ApiEndpoint}/{account.Location}/Accounts/{account.Properties.Id}/Videos/{videoId}/InsightsWidget?{queryParams}";
            var insightsWidgetRequestResult = await _httpClient.GetAsync(requestUrl);
            insightsWidgetRequestResult.VerifyStatus(System.Net.HttpStatusCode.MovedPermanently);
            var insightsWidgetLink = insightsWidgetRequestResult.Headers.Location;
            return insightsWidgetLink;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return null;
    }

    
    public async Task<Uri?> GetPlayerWidgetUrlAsync(string videoId, Account account, string accountAccessToken)
    {
        try
        {
            var requestUrl = $"{ApiEndpoint}/{account.Location}/Accounts/{account.Properties.Id}/Videos/{videoId}/PlayerWidget";
            var playerWidgetRequestResult = await _httpClient.GetAsync(requestUrl);

            var playerWidgetLink = playerWidgetRequestResult.Headers.Location;
            playerWidgetRequestResult.VerifyStatus(System.Net.HttpStatusCode.MovedPermanently);
            return playerWidgetLink;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return null;
    }
        
    private string AddExcludedAIs(string ExcludedAI)
    {
        if (string.IsNullOrEmpty(ExcludedAI))
        {
            return "";
        }
        var list = ExcludedAI.Split(',');
        return list.Aggregate("", (current, item) => current + ("&ExcludedAI=" + item));
    }
}