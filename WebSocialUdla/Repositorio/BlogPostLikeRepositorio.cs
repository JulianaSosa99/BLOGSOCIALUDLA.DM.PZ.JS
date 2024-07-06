
using BloggieWebProject.Data;
using Microsoft.EntityFrameworkCore;
using WebSocialUdla.Models.Dominio;

namespace WebSocialUdla.Repositorio
{
	public class BlogPostLikeRepositorio : IBlogPostLikeRepositorio
	{
		private readonly BlogDbContext blogDbContext;

		public BlogPostLikeRepositorio(BlogDbContext blogDbContext)
        {
			this.blogDbContext = blogDbContext;
		}

		public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
		{
			await blogDbContext.BlogPostLike.AddAsync(blogPostLike);
			await blogDbContext.SaveChangesAsync();
			return blogPostLike;
		}

		public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
		{
			return await blogDbContext.BlogPostLike.Where(x => x.BlogPostId == blogPostId)
				.ToListAsync();
		}

		public async Task<int> GetTotalLikes(Guid blogPostId)
		{
			return await blogDbContext.BlogPostLike
				.CountAsync(x => x.BlogPostId == blogPostId);
		}
	}
}
