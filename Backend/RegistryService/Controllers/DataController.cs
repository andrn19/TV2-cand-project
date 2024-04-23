
namespace TV2.Backend.Services.DatabaseRegistry.Controllers;

using Microsoft.AspNetCore.Mvc;
using ClassLibrary.Models;
using ClassLibrary.Models.Metadata;
using ClassLibrary.Interfaces;
using Interfaces;

[ApiController]
[Route("[controller]")]
public class DataController : ControllerBase, IDatabaseRegistryService, IMetadataService
{
    private readonly ILogger<DataController> _logger;
    private readonly IConsumerRegistry _consumerRegistry;
    private readonly IMessageService _messageService;
    public DataController(ILogger<DataController> logger, IConsumerRegistry consumerRegistry, IMessageService messageService)
    {
        _logger = logger;
        _consumerRegistry = consumerRegistry;
        _messageService = messageService;
    }

    [HttpGet]
    public bool Get()
    {
        return true;
    }

    [HttpPost("AddMetadata/{endpoint}")]
    public bool AddMetadata(Guid endpoint, [FromBody] Video video)
    {
        return _messageService.Enqueue(_consumerRegistry.Resolve(endpoint).Name,video);
    }

    [HttpPut("CreateEndpoint")]
    public bool CreateEndpoint([FromBody] string name)
    {
        return _consumerRegistry.Create(name);
    }

    [HttpPut("UpdateEndpoint")]
    public bool UpdateEndpoint([FromBody] MetadataHost host)
    {
        return _consumerRegistry.Update(host);
    }

    [HttpDelete("DeleteEndpoint")]
    public bool DeleteEndpoint([FromBody] MetadataHost host)
    {
        return _consumerRegistry.Delete(host);
    }

    [HttpGet("ListEndpoints")]
    public IEnumerable<MetadataHost> ListEndpoints()
    {
        return _consumerRegistry.List();
    }
}