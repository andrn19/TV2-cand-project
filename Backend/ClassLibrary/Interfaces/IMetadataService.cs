using TV2.Backend.ClassLibrary.Classes;

namespace TV2.Backend.ClassLibrary.Interfaces;

public interface IMetadataService : IBaseService
{
    public bool AddMetadata(Metadata metadata);
    //public Metadata? GetMetadata(string id);
    //public bool RemoveMetadata(string id);
}