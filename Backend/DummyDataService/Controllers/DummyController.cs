using Microsoft.AspNetCore.Mvc;
namespace DummyDataService.Controllers;

[ApiController]
[Route("[controller]")]
public class DummyController : ControllerBase
{
    private readonly ILogger<DummyController> _logger;

    public DummyController(ILogger<DummyController> logger)
    {
        _logger = logger;
    }
    //[HttpGet(Name = "GetDummyData")]
    [HttpGet]
    public DummyData Get()
    {
        var data = new DummyData("Hello World!");
        return data;
    }
}