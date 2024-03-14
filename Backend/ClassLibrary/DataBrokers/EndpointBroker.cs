using TV2.Backend.ClassLibrary.Classes;
using TV2.Backend.ClassLibrary.Interfaces;

namespace TV2.Backend.ClassLibrary.DataBrokers;

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

    public bool UpdateEndpoint(Guid id, MetadataHost endpoint)
    {
        throw new NotImplementedException();
    }

    public bool DeleteEndpoint(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<KeyValuePair<Guid, string>> ListEndpoints()
    {
        throw new NotImplementedException();
    }
}