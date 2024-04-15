namespace TV2.Backend.ClassLibrary.Interfaces;

using Models.Metadata;

public interface IVideoAnalyserService
{
    Task<string> UploadFootage(string footageUrl, string footageName);
    Task<Metadata> GetMetadata(string footageId);
}