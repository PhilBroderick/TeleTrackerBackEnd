using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeleTracker.CustomResponses;
using TeleTracker.DTOs;

namespace TeleTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetShowByIdAsync(string showID)
        {
            if (string.IsNullOrWhiteSpace(showID))
                return NotFound();
            return Ok(new ShowDTO() { ID = showID });
        }

        [HttpGet]
        public IActionResult GetAllShowsAsync()
        {
            return Ok(new List<ShowDTO>().AsEnumerable());
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