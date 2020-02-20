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