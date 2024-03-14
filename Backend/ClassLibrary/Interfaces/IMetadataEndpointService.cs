using TV2.Backend.ClassLibrary.Classes;

namespace TV2.Backend.ClassLibrary.Interfaces;

public interface IMetadataEndpointService : IBaseService
{
    bool CreateEndpoint(MetadataHost host);
    bool UpdateEndpoint(Guid id, MetadataHost host);
    bool DeleteEndpoint(Guid id);
    IEnumerable<KeyValuePair<Guid, string>> ListEndpoints();
}