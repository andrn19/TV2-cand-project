using TV2.ClassLibrary.Classes;
using TV2.ClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TV2.DataService.Controllers;


[ApiController]
[Route("[controller]")]
public class DataController : ControllerBase, IMetadataEndpointService, IMetadataEndpointResolver
{
    [HttpGet]
    public bool Get()
    {
        return true;
    }

    [HttpPut("CreateEndpoint")]
    public bool CreateEndpoint(MetadataHost host)
    {
        throw new NotImplementedException();
    }

    [HttpPut("UpdateEndpoint")]
    public bool UpdateEndpoint([FromBody] MetadataHost host)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("DeleteEndpoint/{id}")]
    public bool DeleteEndpoint(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpGet("ListEndpoints")]
    public IEnumerable<MetadataHostIdentifier> ListEndpoints()
    {
        
        throw new NotImplementedException();
    }
    
    [HttpPost("ResolveEndpoint")]
    public Task<Uri> Resolve([FromBody] MetadataHostIdentifier identifier)
    {
        throw new NotImplementedException();
    }
}