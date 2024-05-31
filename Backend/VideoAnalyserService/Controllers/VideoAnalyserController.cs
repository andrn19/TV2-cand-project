
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
        var footageId1 = await _analyserService.UploadUrlAsync(footageUrl[0], "footage 7", account, token);
        var footageId2 = await _analyserService.UploadUrlAsync(footageUrl[1], "footage 8", account, token);
        
        string[] footage = {footageId1, footageId2};
        
        var result = await _analyserService.WaitForProgressAsync(footage, account, token);
        //var result2 = await _analyserService.WaitForProgressAsync(footageId2, account, token);
        return result;
    }
    
    
    [HttpPost("ConcurrencyTest")]
    public async Task ConcurrencyTest([FromBody] string[] footageUrl, [FromQuery] int concurrency)
    {
        var videoCount = footageUrl.Length;
        for (int i = 0; i < (videoCount / concurrency); i++)
        {
            var armToken = await _authService.AuthenticateArmAsync();
            var token = await _authService.AuthenticateAsync(armToken);
            var account = await _authService.GetAccountAsync(Consts.ViAccountName, armToken);
            
            string[] footage = [];
            for (int j = 0; j < concurrency; j++)
            {
                var footageId = await _analyserService.UploadUrlAsync(footageUrl[0], $"footage round {i} - video {j}", account, token);
                footage.Append(footageId);
            }
            await _analyserService.WaitForProgressAsync(footage, account, token);
        }
    }
}