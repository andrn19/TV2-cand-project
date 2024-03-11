namespace TV2.DataService.Interfaces;

using ClassLibrary.Classes;

public interface IDataProvider
{
    MetadataHostIdentifier Create(MetadataHost host);
    MetadataHostIdentifier Update(MetadataHost host);
    bool Delete(Guid id);
    IEnumerable<MetadataHostIdentifier> List();
    MetadataHost Resolve(Guid id);
}