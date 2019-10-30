using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeleTracker.DTOs;

namespace TeleTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUserByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();
            return Ok(new UserDTO() { ID = id });
        }
    }
}