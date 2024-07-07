using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebSocialUdla.Dominio.DTOs;

namespace WebSocialUdla.Servicios
{
	public class CommentService : ICommentService
	{
		private readonly HttpClient _httpClient;

		public CommentService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IEnumerable<CommentDto>> GetAllCommentsAsync()
		{
			var response = await _httpClient.GetAsync("api/comments");
			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<IEnumerable<CommentDto>>(content);
		}

		public async Task<CommentDto> GetCommentAsync(Guid id)
		{
			var response = await _httpClient.GetAsync($"api/comments/{id}");
			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<CommentDto>(content);
		}

		public async Task<CommentDto> CreateCommentAsync(CommentDto commentDto)
		{
			var json = JsonConvert.SerializeObject(commentDto);
			var data = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("api/comments", data);
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<CommentDto>(content);
		}

		public async Task<CommentDto> UpdateCommentAsync(Guid id, CommentDto commentDto)
		{
			var json = JsonConvert.SerializeObject(commentDto);
			var data = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PutAsync($"api/comments/{id}", data);
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<CommentDto>(content);
		}

		public async Task<bool> DeleteCommentAsync(Guid id)
		{
			var response = await _httpClient.DeleteAsync($"api/comments/{id}");
			response.EnsureSuccessStatusCode();
			return response.IsSuccessStatusCode;
		}
	}
}

