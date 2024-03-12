using TV2.ClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TV2.ClassLibrary.Classes;

namespace DummyDataService.Controllers;

[ApiController]
[Route("[controller]")]
public class DummyController : ControllerBase, IMetadataService
{
    private readonly ILogger<DummyController> _logger;

    public DummyController(ILogger<DummyController> logger)
    {
        // Normally we would have a data provider here
        _logger = logger;
    }

    [HttpGet]
    public bool Get()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public string? AddMetadata(Metadata metadata)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("dummy-string/{id}")]
    public string? GetMetadataString(string id)
    {
        var data = new Metadata(Guid.Parse(id), "Metadata", "Data has id: " + Guid.Parse(id));
        return data.Description;
    }

    [HttpGet("dummy-data/{id}")]
    public Metadata? GetMetadata(string id)
    {
        var data = new Metadata(Guid.Parse(id), "Metadata", "Data has id: " + Guid.Parse(id));
        return data;
    }

    public bool RemoveMetadata(string id)
    {
        throw new NotImplementedException();
    }
}