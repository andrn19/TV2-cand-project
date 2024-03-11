using TV2.ClassLibrary.Classes;
using TV2.ClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TV2.DataService.Interfaces;

namespace TV2.DataService.Controllers;


[ApiController]
[Route("[controller]")]
public class DataController : ControllerBase, IMetadataEndpointService, IMetadataEndpointResolver
{
    private readonly ILogger<DataController> _logger;
    private readonly IDataProvider _dataProvider;
    public DataController(ILogger<DataController> logger, IDataProvider dataProvider)
    {
        _logger = logger;
        _dataProvider = dataProvider;
        CreateEndpoint(new MetadataHost("Host url", "Host port", "Host name"))
    }

    [HttpGet]
    public bool Get()
    {
        return true;
    }

    [HttpPut("CreateEndpoint")]
    public bool CreateEndpoint(MetadataHost host)
    {
        return _dataProvider.Create(host);
    }

    [HttpPut("UpdateEndpoint/{id}")]
    public bool UpdateEndpoint(Guid id, [FromBody] MetadataHost host)
    {
        return _dataProvider.Update(id, host);
    }

    [HttpDelete("DeleteEndpoint/{id}")]
    public bool DeleteEndpoint(Guid id)
    {
        return _dataProvider.Delete(id);
    }

    [HttpGet("ListEndpoints")]
    public IEnumerable<KeyValuePair<Guid, string>> ListEndpoints()
    {
        
        return _dataProvider.List();
    }
    
    [HttpPost("ResolveEndpoint")]
    public MetadataHost Resolve([FromBody] Guid id)
    {
        return _dataProvider.Resolve(id);
    }
}