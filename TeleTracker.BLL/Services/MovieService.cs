using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TeleTracker.BLL.Interfaces;
using TeleTracker.Core.DTOs;
using TeleTracker.Core.Interfaces;

namespace TeleTracker.BLL.Services
{
    public class MovieService : IMovieService
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly HttpClient _client;

        public MovieService(IServiceConfiguration serviceConfiguration)
        {
            _apiKey = serviceConfiguration.ApiKey;
            _baseUrl = serviceConfiguration.BaseUrl + "movie";
            _client = new HttpClient();
        }

        public async Task<MovieDTO> GetMovieByIdAsync(string id)
        {
            var getUrl = $"{_baseUrl}/{id}?api_key={_apiKey}";
            var response = await _client.GetAsync(getUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var movie = JsonConvert.DeserializeObject<MovieDTO>(content);
                return movie;
            }
            return null;
        }
    }
}
