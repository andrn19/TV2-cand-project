using TV2.Backend.ClassLibrary.Classes;
using TV2.Backend.ClassLibrary.Interfaces;

namespace TV2.Backend.ClassLibrary.DataBrokers;

public class EndpointBroker : BaseBroker, IDatabaseRegistryService
{
    private const string baseUri = "test";

    public bool Get()
    {
        var t = Get<bool>(baseUri+"/Get");
        if (t != null) return t.Result;
        return false;
    }


    public bool CreateEndpoint(string name)
    {
        throw new NotImplementedException();
    }

    public bool UpdateEndpoint(MetadataHost host)
    {
        throw new NotImplementedException();
    }

    public bool DeleteEndpoint(MetadataHost host)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<MetadataHost> ListEndpoints()
    {
        throw new NotImplementedException();
    }
}