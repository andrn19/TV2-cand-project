namespace TV2.Backend.Services.VideoAnalyser.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class VideoAnalyserController : ControllerBase
{
    private readonly ILogger<VideoAnalyserController> _logger;
    public VideoAnalyserController(ILogger<VideoAnalyserController> logger)
    {
        _logger = logger;
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