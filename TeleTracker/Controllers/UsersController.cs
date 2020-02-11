using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeleTracker.Core.DTOs;
using TeleTracker.Core.Interfaces;

namespace TeleTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UsersController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public async  Task<IActionResult> GetUserByIdAsync(string id)
        {
            var user = await _authService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
    }
}