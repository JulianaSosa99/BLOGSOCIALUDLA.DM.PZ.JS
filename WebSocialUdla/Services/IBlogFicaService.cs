using WebSocialUdla.Dominio.DTOs;

namespace WebSocialUdla.Services
{
	public interface IBlogFicaService
	{
		Task<IEnumerable<BlogFicaDto>> GetAllBlogsAsync();
		Task<BlogFicaDto> GetBlogAsync(Guid blogId);
		Task<bool> CreateBlogAsync(BlogFicaDto blogDto);
		Task<bool> UpdateBlogAsync(Guid blogId, BlogFicaDto blogDto);
		Task<bool> DeleteBlogAsync(Guid blogId);
	}
}
