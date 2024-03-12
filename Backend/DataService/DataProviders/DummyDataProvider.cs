using TV2.ClassLibrary.Classes;
using TV2.DataService.Interfaces;

namespace TV2.DataService.DataProviders;

public class DummyDataProvider : IDataProvider
{
    private Dictionary<Guid, MetadataHost> _hosts;
    public DummyDataProvider() : this(new Dictionary<Guid, MetadataHost>()){}
    public DummyDataProvider(Dictionary<Guid, MetadataHost> hosts)
    {
        _hosts = hosts;
    }
    public bool Create(MetadataHost host)
    {
        return _hosts.TryAdd(Guid.NewGuid(), host);
    }

    public bool Update(Guid id, MetadataHost host)
    {
        try
        {
            _hosts[id] = host;
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public bool Delete(Guid id)
    {
        return _hosts.Remove(id);
    }

    public IEnumerable<KeyValuePair<Guid, string>> List()
    {
        return _hosts.Select(pair => new KeyValuePair<Guid, string>(pair.Key, pair.Value.Name)).ToList();
    }

    public MetadataHost Resolve(Guid id)
    {
        try
        {
            return _hosts[id];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}