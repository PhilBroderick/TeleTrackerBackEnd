using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeleTracker.BLL.Interfaces;
using TeleTracker.Core.Common;
using TeleTracker.Core.DTOs;
using TeleTracker.Core.Interfaces;
using TMDbLib.Client;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

namespace TeleTracker.BLL.Services
{
    public class ShowService : IShowService
    {
        private readonly string _apiKey;
        private readonly TMDbClient _client;

        public ShowService(IServiceConfiguration serviceConfiguration)
        {
            _apiKey = serviceConfiguration.ApiKey;
            _client = new TMDbClient(_apiKey);
        }

        public async Task<ShowDTO> GetShowByIdAsync(int id)
        {
            var show = await _client.GetTvShowAsync(id).ConfigureAwait(false);

            if (show is null) return null;

            return new ShowDTO
            {
                ID = show.Id.ToString(),
                Title = show.Name,
                FirstAirDate = show.FirstAirDate.Value,
                Overview = show.Overview,
                InProduction = show.InProduction,
                Seasons = MapSeasons(show.Seasons),
                PosterPath = $"https://image.tmdb.org/t/p/original{show.PosterPath}"
            };
        }

        private IEnumerable<SeasonDTO> MapSeasons(List<SearchTvSeason> seasons)
        {
            foreach (var season in seasons)
            {
                yield return new SeasonDTO
                {
                    SeasonId = season.Id,
                    AirDate = season.AirDate.Value,
                    SeasonNumber = season.SeasonNumber
                };
            }
        }

        public Task<List<ShowDTO>> GetShowsBySearchTerm(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public Task<List<ShowDTO>> GetTopShowsByCriteria(CriteriaValueEnum citeria, TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

        public Task<List<ShowDTO>> GetUserSuscribedShowsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ShowPopularityDTO>> GetMostPopularShows()
        {
            var shows = new List<ShowPopularityDTO>();
            var showResults = await _client.GetTvShowPopularAsync();
            foreach (SearchTv show in showResults.Results)
            {
                shows.Add(new ShowPopularityDTO
                {
                    Name = show.Name,
                    ID = show.Id.ToString(),
                    FirstAirDate = show.FirstAirDate.Value,
                    PosterPath = $"https://image.tmdb.org/t/p/original{show.PosterPath}",
                    Overview = show.Overview,
                    VoteCount = show.VoteCount,
                    Popularity = show.Popularity
                });
            }
            return shows;
        }
    }
}
