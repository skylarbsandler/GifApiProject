using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using GifApiProject.Interfaces;
using GifApiProject.Models;
using System.Text.Json;
using Newtonsoft.Json;

namespace GifApiProject.Services
{
    public class GifsAPIService : IGifsAPIService
    {
        private readonly HttpClient _client;

        public GifsAPIService()
        {
            _client = new HttpClient() { BaseAddress = new Uri("https://localhost:7094") };
        }

        public async Task<List<GifModel>> GetGifs()
        {
            var url = "/gifs";
            var result = new List<GifModel>();
            var response = await _client.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                result = System.Text.Json.JsonSerializer.Deserialize<List<GifModel>>(stringResponse,
                   new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
            return result;
        }

        public async Task<bool> CreateGifAsync(GifModel gif)
        {
            string apiEndpoint = "/gifs";
            var jsonString = JsonConvert.SerializeObject(gif);
            HttpContent jsonGifData = new StringContent(
                jsonString, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(apiEndpoint, jsonGifData);

            return response.IsSuccessStatusCode;
        }
    }
}
