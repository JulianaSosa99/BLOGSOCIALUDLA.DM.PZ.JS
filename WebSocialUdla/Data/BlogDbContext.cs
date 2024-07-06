using BloggieWebProject.Models.Dominio;
using Microsoft.EntityFrameworkCore;
using WebSocialUdla.Models.Dominio;

namespace BloggieWebProject.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {

        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        
        public DbSet<BlogPostLike> BlogPostLike { get; set; }
		public DbSet<BlogPostComment> BlogPostComment { get; set; }



	}
}
