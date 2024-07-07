using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSocialUdla.Dominio.DTOs;

namespace WebSocialUdla.Servicios
{
	public interface IBlogNodoService
	{
		Task<IEnumerable<BlogNodoDto>> GetAllBlogsAsync();
		Task<BlogNodoDto> GetBlogAsync(Guid id);
		Task<BlogNodoDto> CreateBlogAsync(BlogNodoDto blogNodoDto);
		Task<BlogNodoDto> UpdateBlogAsync(Guid id, BlogNodoDto blogNodoDto);
		Task<bool> DeleteBlogAsync(Guid id);
	}
}

