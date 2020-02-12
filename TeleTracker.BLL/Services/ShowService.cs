using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeleTracker.BLL.Interfaces;
using TeleTracker.Core.Common;
using TeleTracker.Core.DTOs;
using TeleTracker.Core.Interfaces;

namespace TeleTracker.BLL.Services
{
    public class ShowService : IShowService
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public ShowService(IServiceConfiguration serviceConfiguration)
        {
            _apiKey = serviceConfiguration.ApiKey;
            _baseUrl = serviceConfiguration.BaseUrl;
        }

        public Task<ShowDTO> GetShowByIdAsync(string id)
        {
            throw new NotImplementedException();
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
