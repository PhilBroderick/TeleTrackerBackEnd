using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TeleTracker.Core.DTOs;
using TeleTracker.Core.Interfaces;
using TeleTracker.Core.Models;

namespace TeleTracker.BLL
{
    public class UserAuthenticationService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;

        public UserAuthenticationService(IAuthRepository authRepository, IConfiguration config)
        {
            _authRepository = authRepository;
            _config = config;
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _authRepository.Login(username, password);

            if (user == null)
                return null;
            return GenerateJWTToken(user);
        }

        private string GenerateJWTToken(User user)
        {
            var claims = new[]
                        {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<UserDTO> Register(string username, string password)
        {
            var user = new User
            {
                Username = username.ToLower()
            };
            var registeredUser = await _authRepository.Register(user, password);
            if (registeredUser != null)
            {
                return new UserDTO
                {
                    ID = registeredUser.ID.ToString(),
                    Username = registeredUser.Username
                };
            }
            else
                return await Task.FromResult(new UserDTO());
        }

        public async Task<bool> UserExists(string username)
        {
            return await _authRepository.UserExists(username.ToLower());
        }
    }
}