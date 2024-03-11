using TV2.ClassLibrary.Classes;
using TV2.DataService.Interfaces;

namespace TV2.DataService.DataProviders;

public class DummyDataProvider : IDataProvider
{
    public Dictionary<Guid, MetadataHost> hosts;
    public bool Create(MetadataHost host)
    {
        return hosts.TryAdd(Guid.NewGuid(), host);
    }

    public bool Update(Guid id, MetadataHost host)
    {
        try
        {
            hosts[id] = host;
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public bool Delete(Guid id)
    {
        return hosts.Remove(id);
    }

    public IEnumerable<KeyValuePair<Guid, string>> List()
    {
        return hosts.Select(pair => new KeyValuePair<Guid, string>(pair.Key, pair.Value.Name)).ToList();
    }

    public MetadataHost Resolve(Guid id)
    {
        try
        {
            return hosts[id];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}