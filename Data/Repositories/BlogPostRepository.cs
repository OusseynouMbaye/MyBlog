using Data.Context;
using Data.Models;

namespace Data.Repositories
{
    public class BlogPostRepository : GenericRepository<BlogPost>
    {
        public BlogPostRepository(BlogDbContext context) : base(context)
        {
        }
    }
}
