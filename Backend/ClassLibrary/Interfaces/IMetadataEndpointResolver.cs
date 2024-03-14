using TV2.Backend.ClassLibrary.Classes;

namespace TV2.Backend.ClassLibrary.Interfaces;

public interface IMetadataEndpointResolver : IBaseService
{
    MetadataHost Resolve(Guid id);
}