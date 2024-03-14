using TV2.Backend.ClassLibrary.Classes;
using TV2.Backend.ClassLibrary.Interfaces;

namespace TV2.Backend.ClassLibrary.DataBrokers;

public class EndpointResolver : BaseBroker, IMetadataEndpointResolver
{
    private const string baseUri = "http://api.data:80/Data";
    
    public bool Get()
    {
        var t = Get<bool>(baseUri+"/Get");
        if (t != null) return t.Result;
        return false;
    }

    public MetadataHost Resolve(Guid id)
    {
        var uri = baseUri + "ResolveEndpoint/" + id;
        var t = Get<MetadataHost>(uri);
        if (t != null) return t.Result;
        return null;
    }
}