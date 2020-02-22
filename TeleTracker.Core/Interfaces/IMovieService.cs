using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeleTracker.Core.DTOs;

namespace TeleTracker.Core.Interfaces
{
    public interface IMovieService
    {
        Task<MovieDTO> GetMovieByIdAsync(string id);
        IAsyncEnumerable<MovieDTO> GetPopularMoviesAsync();
    }
}
