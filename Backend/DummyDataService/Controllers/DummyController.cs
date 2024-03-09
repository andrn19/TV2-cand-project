using ClassLibrary.Classes;
using ClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace DummyDataService.Controllers;

[ApiController]
[Route("[controller]")]
public class DummyController : ControllerBase, IDataService
{
    private readonly ILogger<DummyController> _logger;

    public DummyController(ILogger<DummyController> logger)
    {
        // Normally we would have a data provider here
        _logger = logger;
    }

    [HttpGet]
    bool IBaseService.Get()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public Uri? AddMetadata(Metadata metadata)
    {
        throw new NotImplementedException();
    }

    [HttpGet("dummy-string/{number}")]
    public string? GetMetadataString(int number)
    {
        var data = new Metadata(Guid.Empty, "Metadata", "Data has id: " + number);
        return data.Description;
    }
    
    [HttpGet("dummy-data/{id}")]
    public Metadata? GetMetadata(Guid id)
    {
        var data = new Metadata(id, "Metadata", "Data has id: " + id);
        return data;
    }
}