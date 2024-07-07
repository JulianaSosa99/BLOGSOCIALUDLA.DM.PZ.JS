using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebSocialUdla.Dominio.DTOs;

namespace WebSocialUdla.Servicios
{
	public class BlogNodoService : IBlogNodoService
	{
		private readonly HttpClient _httpClient;

		public BlogNodoService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IEnumerable<BlogNodoDto>> GetAllBlogsAsync()
		{
			return await _httpClient.GetFromJsonAsync<IEnumerable<BlogNodoDto>>("api/nodo/blogs");
		}

		public async Task<BlogNodoDto> GetBlogAsync(Guid id)
		{
			return await _httpClient.GetFromJsonAsync<BlogNodoDto>($"api/nodo/blogs/{id}");
		}

		public async Task<BlogNodoDto> CreateBlogAsync(BlogNodoDto blogNodoDto)
		{
			var response = await _httpClient.PostAsJsonAsync("api/nodo/blogs", blogNodoDto);
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadFromJsonAsync<BlogNodoDto>();
		}

		public async Task<BlogNodoDto> UpdateBlogAsync(Guid id, BlogNodoDto blogNodoDto)
		{
			var response = await _httpClient.PutAsJsonAsync($"api/nodo/blogs/{id}", blogNodoDto);
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadFromJsonAsync<BlogNodoDto>();
		}

		public async Task<bool> DeleteBlogAsync(Guid id)
		{
			var response = await _httpClient.DeleteAsync($"api/nodo/blogs/{id}");
			return response.IsSuccessStatusCode;
		}
	}
}

