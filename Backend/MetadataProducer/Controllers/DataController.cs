namespace TV2.Backend.Services.MetadataProducer.Controllers;

using Microsoft.AspNetCore.Mvc;
using ClassLibrary.Classes;
using TV2.Backend.ClassLibrary.Interfaces;
using Interfaces;

[ApiController]
[Route("[controller]")]
public class DataController : ControllerBase, IMetadataEndpointService, IMetadataEndpointResolver, IMetadataService
{
    private readonly ILogger<DataController> _logger;
    private readonly IDataProvider _dataProvider;
    private readonly IMessageService _messageService;
    public DataController(ILogger<DataController> logger, IDataProvider dataProvider, IMessageService messageService)
    {
        _logger = logger;
        _dataProvider = dataProvider;
        _messageService = messageService;
    }

    [HttpGet]
    public bool Get()
    {
        return true;
    }

    [HttpPost("AddMetadata")]
    public bool AddMetadata([FromBody] Metadata metadata)
    {
        return _messageService.Enqueue(metadata);
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
    
    [HttpPost("ResolveEndpoint/{id}")]
    public MetadataHost Resolve(Guid id)
    {
        return _dataProvider.Resolve(id);
    }
}