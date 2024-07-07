using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebSocialUdla.Dominio.DTOs;

namespace BloggieWebProject.Servicios
{
	public class UserService : IUserService
	{
		private readonly HttpClient _httpClient;

		public UserService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<UserDto> CreateUserAsync(UserDto userDto)
		{
			var response = await _httpClient.PostAsJsonAsync("api/users", userDto);
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadFromJsonAsync<UserDto>();
		}

		public async Task<List<UserDto>> GetAllUsersAsync()
		{
			return await _httpClient.GetFromJsonAsync<List<UserDto>>("api/users");
		}

		public async Task<UserDto> GetUserAsync(Guid id)
		{
			return await _httpClient.GetFromJsonAsync<UserDto>($"api/users/{id}");
		}

		public async Task<UserDto> UpdateUserAsync(Guid id, UserDto userDto)
		{
			var response = await _httpClient.PutAsJsonAsync($"api/users/{id}", userDto);
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadFromJsonAsync<UserDto>();
		}

		public async Task<bool> DeleteUserAsync(Guid id)
		{
			var response = await _httpClient.DeleteAsync($"api/users/{id}");
			return response.IsSuccessStatusCode;
		}
	}
}
