using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeleTracker.BLL;
using TeleTracker.Core.DTOs;
using TeleTracker.Core.Interfaces;

namespace TeleTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDTO userForRegistorDTO)
        {
            if (await _authService.UserExists(userForRegistorDTO.Username))
                return BadRequest("Username already exists.");

            try
            {
                var registeredUser = await _authService.Register(userForRegistorDTO);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error registering user, {ex}.");
            }
        }
    }
}