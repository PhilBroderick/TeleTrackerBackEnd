using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TeleTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        public ActionResult GetShowByIdAsync(string showID)
        {
            if (string.IsNullOrWhiteSpace(showID))
                return NotFound();
            return Ok(showID);
        }
    }
}