using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeleTracker.Core.Common;
using TeleTracker.Core.DTOs;
using TeleTracker.Core.Interfaces;

namespace TeleTracker.BLL.Services
{
    public class TheMovieDB : IShowService
    {
        private readonly string _apiKey;

        public TheMovieDB(string apiKey)
        {
            _apiKey = apiKey;
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
    }
}
