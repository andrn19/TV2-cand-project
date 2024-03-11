using System.Net;
using TV2.ClassLibrary.Classes;
using TV2.ClassLibrary.Interfaces;

namespace TV2.ClassLibrary.DataBrokers;

public class EndpointBroker : BaseBroker, IMetadataEndpointService
{
    private const string baseUri = "test";

    public bool Get()
    {
        var t = Get<bool>(baseUri+"/Get");
        if (t != null) return t.Result;
        return false;
    }

    public bool CreateEndpoint(MetadataHost endpoint)
    {
        throw new NotImplementedException();
    }

    public bool UpdateEndpoint(MetadataHost endpoint)
    {
        throw new NotImplementedException();
    }

    public bool DeleteEndpoint(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<MetadataHostIdentifier> ListEndpoints()
    {
        throw new NotImplementedException();
    }
}