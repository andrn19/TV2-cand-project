namespace TV2.DataService.Interfaces;

using ClassLibrary.Classes;

public interface IDataProvider
{
    bool Create(MetadataHost host);
    bool Update(Guid id, MetadataHost host);
    bool Delete(Guid id);
    IEnumerable<KeyValuePair<Guid, string>> List();
    MetadataHost Resolve(Guid id);
}