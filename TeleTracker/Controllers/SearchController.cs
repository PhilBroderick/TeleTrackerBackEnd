using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TeleTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [Authorize]
        [HttpGet("{searchTerm}")]
        public async Task<IActionResult> Search(string searchTerm)
        {
            return Ok(new List<string> { searchTerm });
        }
    }
}