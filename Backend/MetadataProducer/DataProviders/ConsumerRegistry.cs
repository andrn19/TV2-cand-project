namespace TV2.Backend.Services.MetadataProducer.DataProviders;

using ClassLibrary.Classes;
using Interfaces;

public class ConsumerRegistry : IConsumerRegistry
{
    List<MetadataHost> _hosts;
    
    public ConsumerRegistry() : this(new List<MetadataHost>()){}

    private ConsumerRegistry(List<MetadataHost> hosts)
    {
        _hosts = hosts;
    }
    public bool Create(string name)
    {
        try
        {
            _hosts.Add(new MetadataHost(Guid.NewGuid(), name));
        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public bool Update(MetadataHost host)
    {
        try
        {
            MetadataHost hostToUpdate = _hosts.FirstOrDefault(oldHost => oldHost.Id == host.Id);
            if (hostToUpdate == null) return false;
            hostToUpdate.Name = host.Name;
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public bool Delete(MetadataHost host)
    {
        return _hosts.Remove(_hosts.FirstOrDefault(hostToBeRemoved => (hostToBeRemoved.Id == host.Id) &&(hostToBeRemoved.Name == host.Name)));
    }

    public IEnumerable<MetadataHost> List()
    {
        return _hosts;
    }

    public MetadataHost Resolve(Guid id)
    {
        return _hosts.FirstOrDefault(host => host.Id == id);
    }
}