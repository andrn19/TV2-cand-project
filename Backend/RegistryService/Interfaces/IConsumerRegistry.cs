namespace TV2.Backend.Services.DatabaseRegistry.Interfaces;

using ClassLibrary.Models;

public interface IConsumerRegistry
{
    bool Create(string name);
    bool Update(MetadataHost host);
    bool Delete(MetadataHost host);
    IEnumerable<MetadataHost> List();
    MetadataHost? Resolve(Guid id);
}