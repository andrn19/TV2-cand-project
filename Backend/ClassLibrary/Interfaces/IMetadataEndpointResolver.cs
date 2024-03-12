using TV2.ClassLibrary.Classes;

namespace TV2.ClassLibrary.Interfaces;

public interface IMetadataEndpointResolver : IBaseService
{
    MetadataHost Resolve(Guid id);
}