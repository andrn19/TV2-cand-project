using System;
using ClassLibrary.Classes;

namespace ClassLibrary.Interfaces;

public interface IDataService : IBaseService
{
    public Uri? AddMetadata(Metadata metadata);
    public Metadata? GetMetadata(Guid id);
}