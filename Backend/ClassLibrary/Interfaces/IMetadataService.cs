namespace TV2.Backend.ClassLibrary.Interfaces;

using Models.Metadata;

public interface IMetadataService : IBaseService
{
    bool AddMetadata(Guid endpoint, Video video);
    //public Metadata? GetMetadata(string id);
    //public bool RemoveMetadata(string id);
}