using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeleTracker.CustomResponses;
using TeleTracker.Core.DTOs;

namespace TeleTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetShowByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();
            return Ok(new ShowDTO() { ID = id, Title = $"Show {id}" });
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
    }
}