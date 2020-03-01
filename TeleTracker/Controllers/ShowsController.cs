using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TeleTracker.CustomResponses;
using TeleTracker.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using TeleTracker.Core.Interfaces;
using System.Threading.Tasks;
using System.Security.Claims;
using TeleTracker.Helpers;

namespace TeleTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IShowService _showService;
        private readonly ISubscriptionService _subService;

        public ShowsController(IShowService showService, ISubscriptionService subService)
        {
            _showService = showService;
            _subService = subService;
        }

        [HttpGet("{id:int}")]
        [Authorize]
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

        [HttpPost("{id}/subscribe")]
        [Authorize]
        public async Task<IActionResult> SubscribeToShowAsync(string id)
        {
            var userId = ControllerContextHelper.GetCurrentUserID(HttpContext);
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(userId))
                return NotFound(new SubscribeToShowResponse(userId, id, false));
            var showSubscription = await GetShowSubscription(id);
            await _subService.InitializeService();
            return Ok(new SubscribeToShowResponse(userId, id, await _subService.SubscribeToShow(showSubscription, userId)));
        }

        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularShowsAsync()
        {
            var shows = await _showService.GetMostPopularShows();
            if (shows == null) return BadRequest();
            return Ok(shows);
        }

        [HttpGet("subscribed")]
        [Authorize]
        public async Task<IActionResult> GetSubscribedShowsAsync()
        {
            var userId = ControllerContextHelper.GetCurrentUserID(HttpContext);
            if (string.IsNullOrWhiteSpace(userId))
                return NotFound();
            await _subService.InitializeService();
            return Ok(await _subService.GetSubscribedShows(userId));
        }

        [HttpGet("{id}/issubscribed")]
        public async Task<IActionResult> IsSubscribedToShowAsync(string id)
        {
            if (id == "456" || id == "4194")
                return Ok(true);
            return Ok(false);
        }

        private async Task<ShowSubscriptionDTO> GetShowSubscription(string id)
        {
            var show = await _showService.GetShowByIdAsync(int.Parse(id));
            return new ShowSubscriptionDTO
            {
                Id = id,
                Title = show.Title
            };
        }
    }
}