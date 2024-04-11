
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

    [HttpPost("AddMetadata")]
    public async Task<string> UploadFootage([FromQuery] string footageUrl, [FromQuery] string footageName)
    {
        var armToken = await _authService.AuthenticateArmAsync();
        var token = await _authService.AuthenticateAsync(armToken);
        var account = await _authService.GetAccountAsync(Consts.ViAccountName, armToken);
        var videoId = await _analyserService.UploadUrlAsync(footageUrl, footageName, account, token);
        return videoId;
    }

    [HttpPost("GetMetadata/{footageId}")]
    public async Task<Metadata> GetMetadata(string footageId)
    {
        var armToken = await _authService.AuthenticateArmAsync();
        var token = await _authService.AuthenticateAsync(armToken);
        var account = await _authService.GetAccountAsync(Consts.ViAccountName, armToken);
        var index = await _analyserService.WaitForIndexAsync(footageId, account, token);
        return index.Videos[0].Metadata;

    }
}