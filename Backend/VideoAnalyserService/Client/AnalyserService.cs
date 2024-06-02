namespace TV2.Backend.Services.VideoAnalyser.Client;

using System.Text.Json;
using Model;
using Utils;
using Interfaces;
using static Consts;

public class AnalyserService : IAnalyserService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AnalyserService>? _logger;
    private readonly TimeSpan _pollingInterval = TimeSpan.FromSeconds(10);
    private string schema = "faces,topics,labels,keywords,namedLocations,namedPeople,shots,transcript";

    public AnalyserService(ILogger<AnalyserService>? logger = null, HttpClient? httpClient = null)
    {
        System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;
        _httpClient = httpClient;
        if (httpClient == null) _httpClient = HttpClientUtils.CreateHttpClient();
        _logger = logger;
    }
    
    public async Task<string> UploadUrlAsync(string videoUrl, string videoName, Account account,
        string accountAccessToken, string exludedAIs = null, bool waitForIndex = false)
    {
        try
        {
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
            
            var url = $"{ApiEndpoint}/{account.Location}/Accounts/{account.Properties.Id}/Videos?{queryParams}";
            var uploadRequestResult = await _httpClient.PostAsync(url, null);
            uploadRequestResult.VerifyStatus(System.Net.HttpStatusCode.OK);
            var uploadResult = await uploadRequestResult.Content.ReadAsStringAsync();
            
            var videoId = JsonSerializer.Deserialize<Index>(uploadResult).Id;
            
            return videoId;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    
    public async Task<Index> WaitForIndexAsync(string videoId, Account account, string accountAccessToken, string schema)
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
            
            if (processingState == ProcessingState.Processed.ToString())
            {
                return JsonSerializer.Deserialize<Index>(videoGetIndexResult);
            }
            if (processingState == ProcessingState.Failed.ToString())
            {
                throw new Exception(videoGetIndexResult);
            }
            
            Console.WriteLine($"The video index state is {processingState}");
            await Task.Delay(_pollingInterval);
        }
    }
    
    
    
    public async Task<bool> WaitForProgressAsync(List<string> videoIds, Account account, string accountAccessToken)
    {
        var interval = TimeSpan.FromSeconds(2);
        Dictionary<string, bool> My_dict = new Dictionary<string, bool>();

        foreach (var videoId in videoIds)
        {
            My_dict.Add(videoId, false);
            Console.WriteLine($"{videoId} Added");
        }
        
        while (true)
        {
            foreach (var videoId in videoIds)
            {
                if (My_dict[videoId] == false)
                {
                    var queryParams = new Dictionary<string, string>()
                    {
                        {"language", "English"},
                        {"includeSummarizedInsights", "false"},
                        {"accessToken", accountAccessToken}
                    }.CreateQueryString();

                    var requestUrl = $"{ApiEndpoint}/{account.Location}/Accounts/{account.Properties.Id}/Videos/{videoId}/Index?{queryParams}";
                    var videoGetIndexRequestResult = await _httpClient.GetAsync(requestUrl);
                    videoGetIndexRequestResult.VerifyStatus(System.Net.HttpStatusCode.OK);
                    var videoGetIndexResult = await videoGetIndexRequestResult.Content.ReadAsStringAsync();
                    string processingState = JsonSerializer.Deserialize<Index>(videoGetIndexResult).State;
                    string progressState = JsonSerializer.Deserialize<Index>(videoGetIndexResult).Videos[0].Progress;
                    
                    if (processingState == ProcessingState.Processed.ToString())
                    {
                        My_dict[videoId] = true;
                    }
                    
                    if (processingState == ProcessingState.Failed.ToString())
                    {
                        throw new Exception(videoGetIndexResult);
                    }
                    
                    Console.WriteLine($"Video {videoId} index {processingState} at {progressState}");
                    
                    await Task.Delay(interval);
                }
            }
            if (My_dict.Values.All(value => value))
            {
                return true;
            }
        }
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