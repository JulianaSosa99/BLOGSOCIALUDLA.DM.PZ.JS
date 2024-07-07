using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebSocialUdla.Dominio.DTOs;

namespace WebSocialUdla.Servicios
{
	public class TagService : ITagService
	{
		private readonly HttpClient _httpClient;

		public TagService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IEnumerable<TagDto>> GetAllTagsAsync()
		{
			var response = await _httpClient.GetAsync("api/tags");
			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<IEnumerable<TagDto>>(content);
		}

		public async Task<TagDto> GetTagAsync(Guid id)
		{
			var response = await _httpClient.GetAsync($"api/tags/{id}");
			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<TagDto>(content);
		}

		public async Task<TagDto> CreateTagAsync(TagDto tagDto)
		{
			var json = JsonConvert.SerializeObject(tagDto);
			var data = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("api/tags", data);
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<TagDto>(content);
		}

		public async Task<TagDto> UpdateTagAsync(Guid id, TagDto tagDto)
		{
			var json = JsonConvert.SerializeObject(tagDto);
			var data = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PutAsync($"api/tags/{id}", data);
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<TagDto>(content);
		}

		public async Task<bool> DeleteTagAsync(Guid id)
		{
			var response = await _httpClient.DeleteAsync($"api/tags/{id}");
			response.EnsureSuccessStatusCode();
			return response.IsSuccessStatusCode;
		}
	}
}

