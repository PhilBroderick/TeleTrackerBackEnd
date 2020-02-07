using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeleTracker.Core.Interfaces;
using TeleTracker.Models;

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
                var registeredUser = await _authService.Register(userForRegistorDTO.Username, userForRegistorDTO.Password);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error registering user, {ex}.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDTO userForLoginDTO)
        {
            var token = await _authService.Login(userForLoginDTO.Username, userForLoginDTO.Password);
            if (token == null || string.IsNullOrWhiteSpace(token))
                return Unauthorized();
            return Ok(new
            {
                token
            });
        }
    }
}