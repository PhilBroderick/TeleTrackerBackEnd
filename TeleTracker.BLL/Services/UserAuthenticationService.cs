using System;
using System.Collections.Generic;
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

        public UserAuthenticationService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public Task<UserDTO> Login(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> Register(UserForRegisterDTO userDTO)
        {
            var user = new User
            {
                Username = userDTO.Username.ToLower()
            };
            var registeredUser = await _authRepository.Register(user, userDTO.Password);
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