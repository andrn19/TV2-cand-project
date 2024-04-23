namespace TV2.Backend.ClassLibrary.Interfaces;

using Models;

public interface IDatabaseRegistryService : IBaseService
{
    bool CreateEndpoint(string name);
    bool UpdateEndpoint(MetadataHost host);
    bool DeleteEndpoint(MetadataHost host);
    IEnumerable<MetadataHost> ListEndpoints();
}
