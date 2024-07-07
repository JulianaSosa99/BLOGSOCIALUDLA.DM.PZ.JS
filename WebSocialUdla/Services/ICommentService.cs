using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSocialUdla.Dominio.DTOs;

namespace WebSocialUdla.Servicios
{
	public interface ICommentService
	{
		Task<IEnumerable<CommentDto>> GetAllCommentsAsync();
		Task<CommentDto> GetCommentAsync(Guid id);
		Task<CommentDto> CreateCommentAsync(CommentDto commentDto);
		Task<CommentDto> UpdateCommentAsync(Guid id, CommentDto commentDto);
		Task<bool> DeleteCommentAsync(Guid id);
	}
}

