using TV2.Backend.ClassLibrary.Classes;

namespace TV2.Backend.ClassLibrary.Interfaces;

public interface IMetadataEndpointService : IBaseService
{
    bool CreateEndpoint(string name);
    bool UpdateEndpoint(MetadataHost host);
    bool DeleteEndpoint(MetadataHost host);
    IEnumerable<MetadataHost> ListEndpoints();
}
