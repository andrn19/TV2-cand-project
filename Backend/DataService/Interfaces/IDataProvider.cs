namespace TV2.DataService.Interfaces;

using ClassLibrary.Classes;

public interface IDataProvider
{
    MetadataHostIdentifier Create(MetadataHost host);
    MetadataHostIdentifier Update(MetadataHost host);
    bool Delete(MetadataHost host);
    IEnumerable<MetadataHostIdentifier> List();
    MetadataHostIdentifier Resolve(Guid id);
}