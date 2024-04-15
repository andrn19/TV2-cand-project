namespace TV2.Backend.ClassLibrary.Interfaces;

using Models.Metadata;

public interface IMetadataService : IBaseService
{
    bool AddMetadata(Guid endpoint, Metadata metadata);
    //public Metadata? GetMetadata(string id);
    //public bool RemoveMetadata(string id);
}