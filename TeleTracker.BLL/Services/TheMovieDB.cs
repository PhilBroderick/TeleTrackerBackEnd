using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeleTracker.BLL.Interfaces;
using TeleTracker.Core.Common;
using TeleTracker.Core.DTOs;

namespace TeleTracker.BLL.Services
{
    public class TheMovieDB : IShowAPI
    {
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
