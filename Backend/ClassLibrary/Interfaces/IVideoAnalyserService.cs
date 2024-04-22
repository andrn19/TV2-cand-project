namespace TV2.Backend.ClassLibrary.Interfaces;

using Models.Metadata;

public interface IVideoAnalyserService
{
    Task<string> UploadFootage(string footageUrl, string footageName);
    Task<Video> GetMetadata(string footageId, string schema);
}