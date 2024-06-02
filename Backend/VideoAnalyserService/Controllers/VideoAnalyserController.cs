
using TV2.Backend.ClassLibrary.Models.Metadata;

namespace TV2.Backend.Services.VideoAnalyser.Controllers;

using Interfaces;
using ClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class VideoAnalyserController : ControllerBase, IVideoAnalyserService
{
    private readonly ILogger<VideoAnalyserController> _logger;
    private readonly IAuthService _authService;
    private readonly IAnalyserService _analyserService;
    
    public VideoAnalyserController(ILogger<VideoAnalyserController> logger, IAuthService authService, IAnalyserService analyserService)
    {
        _logger = logger;
        _authService = authService;
        _analyserService = analyserService;
    }
    
    [HttpGet]
    public bool Get()
    {
        return true;
    }

    [HttpPost("UploadFootage")]
    public async Task<string> UploadFootage([FromQuery] string footageUrl, [FromQuery] string footageName)
    {
        var armToken = await _authService.AuthenticateArmAsync();
        var token = await _authService.AuthenticateAsync(armToken);
        var account = await _authService.GetAccountAsync(Consts.ViAccountName, armToken);
        var videoId = await _analyserService.UploadUrlAsync(footageUrl, footageName, account, token);
        return videoId;
    }

    [HttpPost("GetMetadata/{footageId}")]
    public async Task<Video> GetMetadata(string footageId, [FromBody] string schema)
    {
        var armToken = await _authService.AuthenticateArmAsync();
        var token = await _authService.AuthenticateAsync(armToken);
        var account = await _authService.GetAccountAsync(Consts.ViAccountName, armToken);
        var index = await _analyserService.WaitForIndexAsync(footageId, account, token, schema);
        var video = index.Videos[0];
        video.Name = index.Name;
        return video;

    }
    
    [HttpPost("GetProgress")]
    public async Task<bool> GetProgress([FromBody] string[] footageUrl)
    {
        var armToken = await _authService.AuthenticateArmAsync();
        var token = await _authService.AuthenticateAsync(armToken);
        var account = await _authService.GetAccountAsync(Consts.ViAccountName, armToken);
        
        var footage = new List<string>();
        
        var footageId1 = await _analyserService.UploadUrlAsync(footageUrl[0], "footage 7", account, token);
        var footageId2 = await _analyserService.UploadUrlAsync(footageUrl[1], "footage 8", account, token);
        
        footage.Add(footageId1);
        footage.Add(footageId2);
        
        var result = await _analyserService.WaitForProgressAsync(footage, account, token);
        return result;
    }
    
    
    [HttpPost("ConcurrencyTest")]
    public async Task ConcurrencyTest([FromBody] string[] footageUrl, [FromQuery] int concurrency)
    {
        var interval = TimeSpan.FromMilliseconds(500);
        var testId = Guid.NewGuid();
        var videoCount = footageUrl.Length;
        var footageUrlCount = 0;
        for (int i = 0; i < (videoCount / concurrency); i++)
        {
            var armToken = await _authService.AuthenticateArmAsync();
            var token = await _authService.AuthenticateAsync(armToken);
            var account = await _authService.GetAccountAsync(Consts.ViAccountName, armToken);
            
            var footage = new List<string>();
            for (int j = 0; j < concurrency; j++)
            {
                var name = $"test-round-{i + 1}-video-{j + 1}-{testId}";
                Console.WriteLine($"Indexing {name}");
                var footageId = await _analyserService.UploadUrlAsync(footageUrl[footageUrlCount], name, account, token);
                Console.WriteLine($"{footageId}");
                footage.Add(footageId);
                footageUrlCount++;
                await Task.Delay(interval);
            }
            Console.WriteLine("Awaiting index");
            await _analyserService.WaitForProgressAsync(footage, account, token);
            Console.WriteLine($"Round {i+1} finished Indexing");
        }
    }
}