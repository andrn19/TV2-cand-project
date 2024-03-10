namespace TV2.ClassLibrary.Interfaces;

public interface IMetadataEndpointService
{
    public Gateway? GetMetadata(string id);
}