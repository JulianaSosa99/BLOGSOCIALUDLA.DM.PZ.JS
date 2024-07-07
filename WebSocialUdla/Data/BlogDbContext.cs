//using BloggieWebProject.Models.Dominio;
//using Microsoft.EntityFrameworkCore;
//using WebSocialUdla.Models.Dominio;

//namespace BloggieWebProject.Data
//{
//    public class BlogDbContext : DbContext
//    {
//        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
//        {

//        }

//        public DbSet<BlogPost> BlogPosts { get; set; }
//        public DbSet<Tag> Tags { get; set; }

//        public DbSet<BlogPostLike> BlogPostLike { get; set; }
//		public DbSet<BlogPostComment> BlogPostComment { get; set; }



//	}
//}
using Microsoft.EntityFrameworkCore;
using WebSocialUdla.Dominio.DTOs;

namespace WebSocialUdla.Data
{
	public class BlogDbContext : DbContext
	{
		public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
		{
		}

		public DbSet<BlogFicaDto> BlogFica { get; set; }
		public DbSet<BlogNodoDto> BlogNodo { get; set; }
		public DbSet<TagDto> Tags { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			
		}
	}
}
