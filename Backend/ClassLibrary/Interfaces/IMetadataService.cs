using TV2.ClassLibrary.Classes;

namespace TV2.ClassLibrary.Interfaces;

public interface IMetadataService : IBaseService
{
    public string? AddMetadata(Metadata metadata);
    public Metadata? GetMetadata(string id);
    public bool RemoveMetadata(string id);
}