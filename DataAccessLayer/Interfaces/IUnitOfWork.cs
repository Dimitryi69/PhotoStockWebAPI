using DataAccessLayer.Entities;
using System;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Author> Authors { get; }
        IRepository<Photo> Photos { get; }
        IRepository<Text> Texts { get; }
        void Save();
    }
}
