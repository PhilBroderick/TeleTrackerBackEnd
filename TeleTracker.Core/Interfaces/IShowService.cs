using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeleTracker.Core.Common;
using TeleTracker.Core.DTOs;

namespace TeleTracker.Core.Interfaces
{
    public interface IShowService
    {
        Task<List<ShowDTO>> GetUserSuscribedShowsAsync();

        Task<List<ShowDTO>> GetShowsBySearchTerm(string searchTerm);

        Task<List<ShowDTO>> GetTopShowsByCriteria(CriteriaValueEnum citeria, TimeSpan timeSpan);
    }
}
