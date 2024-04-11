namespace TV2.Backend.ClassLibrary.DataBrokers;

using Interfaces;
using Models.Metadata;

public class MetadataBroker : BaseBroker, IMetadataService
{
    public bool Get()
    {
        throw new NotImplementedException();
    }

    public bool AddMetadata(Guid id, Metadata metadata)
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