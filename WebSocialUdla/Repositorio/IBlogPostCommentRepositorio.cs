using WebSocialUdla.Models.Dominio;

namespace WebSocialUdla.Repositorio
{
	public interface IBlogPostCommentRepositorio
	{
		Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);
		Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId);
	}
}
