using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebSocialUdla.Dominio.DTOs;
using WebSocialUdla.Services;

public class BlogFicaService : IBlogFicaService
{
	private readonly HttpClient _httpClient;

	public BlogFicaService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<IEnumerable<BlogFicaDto>> GetAllBlogsAsync()
	{
		var response = await _httpClient.GetAsync("api/fica/blogs"); // Ruta para obtener todos los blogs
		response.EnsureSuccessStatusCode();

		return await response.Content.ReadFromJsonAsync<IEnumerable<BlogFicaDto>>();
	}

	public async Task<BlogFicaDto> GetBlogAsync(Guid blogId)
	{
		var response = await _httpClient.GetAsync($"api/fica/blogs/{blogId}"); // Ruta para obtener un blog por ID
		response.EnsureSuccessStatusCode();

		return await response.Content.ReadFromJsonAsync<BlogFicaDto>();
	}

	public async Task<bool> CreateBlogAsync(BlogFicaDto blogDto)
	{
		var response = await _httpClient.PostAsJsonAsync("api/fica/blogs", blogDto); // Ruta para crear un nuevo blog
		response.EnsureSuccessStatusCode();

		return response.IsSuccessStatusCode;
	}

	public async Task<bool> UpdateBlogAsync(Guid blogId, BlogFicaDto blogDto)
	{
		var response = await _httpClient.PutAsJsonAsync($"api/fica/blogs/{blogId}", blogDto); // Ruta para actualizar un blog por ID
		response.EnsureSuccessStatusCode();

		return response.IsSuccessStatusCode;
	}

	public async Task<bool> DeleteBlogAsync(Guid blogId)
	{
		var response = await _httpClient.DeleteAsync($"api/fica/blogs/{blogId}"); // Ruta para eliminar un blog por ID
		response.EnsureSuccessStatusCode();

		return response.IsSuccessStatusCode;
	}
}
