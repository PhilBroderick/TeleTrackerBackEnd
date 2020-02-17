using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TeleTracker.CustomResponses;
using TeleTracker.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using TeleTracker.Core.Interfaces;
using System.Threading.Tasks;

namespace TeleTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IShowService _showService;

        public ShowsController(IShowService showService)
        {
            _showService = showService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetShowByIdAsync(int id)
        {
            if (id <= 0)
                return NotFound();

            var show = await _showService.GetShowByIdAsync(id).ConfigureAwait(false);
            return Ok(show);
        }

        [HttpGet]
        public IActionResult GetAllShowsAsync()
        {
            var shows = new List<ShowDTO>()
            { new ShowDTO { ID = "1", Title = "Show 1"},
            new ShowDTO { ID = "2", Title = "Show 2"},
            new ShowDTO { ID = "3", Title = "Show 3"},
            new ShowDTO { ID = "4", Title = "Show 4"}
            };
            return Ok(shows.AsEnumerable());
        }

        [HttpPost]
        public IActionResult SubscribeToShowAsync(string showID)
        {
            if (string.IsNullOrWhiteSpace(showID))
                return NotFound(new SubscribeToShowResponse("123", showID, false));
            return Ok(new SubscribeToShowResponse("123", showID, true));
        }

        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularShowsAsync()
        {
            var shows = await _showService.GetMostPopularShows();
            if (shows == null) return BadRequest();
            return Ok(shows);
        }
    }
}