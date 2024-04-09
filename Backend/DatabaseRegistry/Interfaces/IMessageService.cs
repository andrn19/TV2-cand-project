namespace TV2.Backend.Services.MetadataProducer.Interfaces;
using ClassLibrary.Classes;


public interface IMessageService
{
    bool Enqueue(string route, Metadata message);
}