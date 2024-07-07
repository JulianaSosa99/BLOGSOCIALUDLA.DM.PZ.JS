using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSocialUdla.Dominio.DTOs;

namespace BloggieWebProject.Servicios
{
	public interface IUserService
	{
		Task<UserDto> CreateUserAsync(UserDto userDto);
		Task<List<UserDto>> GetAllUsersAsync();
		Task<UserDto> GetUserAsync(Guid id);
		Task<UserDto> UpdateUserAsync(Guid id, UserDto userDto);
		Task<bool> DeleteUserAsync(Guid id);
	}
}
