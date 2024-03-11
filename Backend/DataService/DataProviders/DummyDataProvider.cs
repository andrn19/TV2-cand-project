using TV2.ClassLibrary.Classes;
using TV2.DataService.Interfaces;

namespace TV2.DataService.DataProviders;

public class DummyDataProvider : IDataProvider
{
    public MetadataHostIdentifier Create(MetadataHost host)
    {
        throw new NotImplementedException();
    }

    public MetadataHostIdentifier Update(MetadataHost host)
    {
        throw new NotImplementedException();
    }

    public bool Delete(MetadataHost host)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<MetadataHostIdentifier> List()
    {
        throw new NotImplementedException();
    }

    public MetadataHost Resolve(Guid id)
    {
        throw new NotImplementedException();
    }
}