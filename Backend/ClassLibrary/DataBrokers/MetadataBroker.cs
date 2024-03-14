using TV2.Backend.ClassLibrary.Classes;
using TV2.Backend.ClassLibrary.Interfaces;

namespace TV2.Backend.ClassLibrary.DataBrokers;

public class MetadataBroker : BaseBroker, IMetadataService
{
    public bool Get()
    {
        throw new NotImplementedException();
    }

    public string? AddMetadata(Metadata metadata)
    {
        throw new NotImplementedException();
    }

    public Metadata? GetMetadata(string id)
    {
        throw new NotImplementedException();
    }

    public bool RemoveMetadata(string id)
    {
        throw new NotImplementedException();
    }
}