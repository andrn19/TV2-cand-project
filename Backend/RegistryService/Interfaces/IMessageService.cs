namespace TV2.Backend.Services.DatabaseRegistry.Interfaces;

using ClassLibrary.Models.Metadata;


public interface IMessageService
{
    bool Enqueue(string route, Video video);
}