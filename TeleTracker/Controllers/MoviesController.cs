using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeleTracker.Core.DTOs;
using TeleTracker.Core.Interfaces;

namespace TeleTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        
        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularMovies()
        {
            var movieList = new List<MovieDTO>();
            await foreach(var movie in _movieService.GetPopularMoviesAsync())
            {
                movieList.Add(movie);
            }
            if (movieList.Count == 0)
                return NotFound();
            return Ok(movieList);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetMovieByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();
            var movie = await _movieService.GetMovieByIdAsync(id).ConfigureAwait(false);
            if (movie == null)
                return NotFound();
            return Ok(movie);
        }
    }
}