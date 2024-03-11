using TV2.ClassLibrary.Classes;
using TV2.ClassLibrary.Interfaces;

namespace TV2.ClassLibrary.DataBrokers;

public class MetadataBroker : BaseBroker, IMetadataService
{
    private ApiEndpoint BaseUri { get; set; }
    public MetadataBroker(ApiEndpoint baseUri)
    {
        this.BaseUri = baseUri;
    }

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