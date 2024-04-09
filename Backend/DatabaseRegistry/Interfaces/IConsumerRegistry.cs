namespace TV2.Backend.Services.DatabaseRegistry.Interfaces;

using ClassLibrary.Classes;

public interface IConsumerRegistry
{
    bool Create(string name);
    bool Update(MetadataHost host);
    bool Delete(MetadataHost host);
    IEnumerable<MetadataHost> List();
    MetadataHost? Resolve(Guid id);
}