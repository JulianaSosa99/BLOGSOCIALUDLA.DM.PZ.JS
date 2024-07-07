using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSocialUdla.Dominio.DTOs;

namespace WebSocialUdla.Servicios
{
	public interface ITagService
	{
		Task<IEnumerable<TagDto>> GetAllTagsAsync();
		Task<TagDto> GetTagAsync(Guid id);
		Task<TagDto> CreateTagAsync(TagDto tagDto);
		Task<TagDto> UpdateTagAsync(Guid id, TagDto tagDto);
		Task<bool> DeleteTagAsync(Guid id);
	}
}
