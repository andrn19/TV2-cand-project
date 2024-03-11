using TV2.ClassLibrary.Classes;

namespace TV2.ClassLibrary.Interfaces;

public interface IMetadataEndpointService : IBaseService
{
    public bool CreateEndpoint(MetadataHost host);
    public bool UpdateEndpoint(MetadataHost host);
    public bool DeleteEndpoint(Guid id);
    public IEnumerable<MetadataHostIdentifier> ListEndpoints();
}