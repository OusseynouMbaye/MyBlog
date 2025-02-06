using Data.Context;
using Data.Models;
using Data.Repositories;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogDbContext _context;

        public IGenericRepository<BlogPost> BlogPosts { get; }
        public IGenericRepository<Category> Categories { get; }
        public IGenericRepository<Tag> Tags { get; }
        public IGenericRepository<Comment> Comments { get; }

        public UnitOfWork(BlogDbContext context)
        {
            _context = context;
            BlogPosts = new GenericRepository<BlogPost>(_context);
            Categories = new GenericRepository<Category>(_context);
            Tags = new GenericRepository<Tag>(_context);
            Comments = new GenericRepository<Comment>(_context);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
