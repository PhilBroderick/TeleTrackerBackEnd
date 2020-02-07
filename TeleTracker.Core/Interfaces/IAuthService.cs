using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeleTracker.Core.DTOs;
using TeleTracker.Core.Models;

namespace TeleTracker.Core.Interfaces
{
    public interface IAuthService
    {
        Task<UserDTO> Register(UserForRegisterDTO user);

        Task<UserDTO> Login(UserDTO user);

        Task<bool> UserExists(string username);
    }
}