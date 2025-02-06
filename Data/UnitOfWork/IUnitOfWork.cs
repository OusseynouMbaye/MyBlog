using Data.Models;
using Data.Repositories;

namespace Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<BlogPost> BlogPosts { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Tag> Tags { get; }
        IGenericRepository<Comment> Comments { get; }

        Task SaveChangesAsync();
    }
}
