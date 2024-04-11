using TV2.Backend.Services.VideoAnalyser.Interfaces;

namespace TV2.Backend.Services.VideoAnalyser.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class VideoAnalyserController : ControllerBase
{
    private readonly ILogger<VideoAnalyserController> _logger;
    private readonly IAuthService _authService;
    private readonly IVideoAnalyserService _videoAnalyserService;
    
    public VideoAnalyserController(ILogger<VideoAnalyserController> logger, IAuthService authService, IVideoAnalyserService videoAnalyserService)
    {
        _logger = logger;
        _authService = authService;
        _videoAnalyserService = videoAnalyserService;
    }
    
    [HttpGet]
    public bool Get()
    {
        return true;
    }
    
    [HttpPost("AddMetadata/{endpoint}")]
    public bool AddMetadata(Guid endpoint)
    {
        return true;
    }
}