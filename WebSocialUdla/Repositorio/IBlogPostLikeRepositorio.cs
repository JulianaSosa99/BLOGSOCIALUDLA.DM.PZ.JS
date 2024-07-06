using WebSocialUdla.Models.Dominio;

namespace WebSocialUdla.Repositorio
{
	public interface IBlogPostLikeRepositorio
	{
		Task<int> GetTotalLikes(Guid blogPostId);
		Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);
		Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike );
	}
}
