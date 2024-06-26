using BloggieWebProject.Models.Dominio;
using Microsoft.EntityFrameworkCore;

namespace BloggieWebProject.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }


    }
}
