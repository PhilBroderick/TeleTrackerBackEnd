using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeleTracker.Core.DTOs;
using TeleTracker.Core.Interfaces;

namespace TeleTracker.BLL.Services
{
    public class MovieService : IMovieService
    {
        public Task<MovieDTO> GetMovieByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
