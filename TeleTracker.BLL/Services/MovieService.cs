using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TeleTracker.BLL.Interfaces;
using TeleTracker.Core.DTOs;
using TeleTracker.Core.Interfaces;
using TMDbLib.Client;

namespace TeleTracker.BLL.Services
{
    public class MovieService : IMovieService
    {
        private readonly string _apiKey;
        private readonly TMDbClient _client;

        public MovieService(IServiceConfiguration serviceConfiguration)
        {
            _apiKey = serviceConfiguration.ApiKey;
            _client = new TMDbClient(_apiKey);
        }

        public async Task<MovieDTO> GetMovieByIdAsync(string id)
        {
            var movie = await _client.GetMovieAsync(int.Parse(id)).ConfigureAwait(false);
            if (movie is null) return null;
            return new MovieDTO
            {
                ID = movie.Id.ToString(),
                Title = movie.Title,
                Language = movie.OriginalLanguage
            };
        }
    }
}
