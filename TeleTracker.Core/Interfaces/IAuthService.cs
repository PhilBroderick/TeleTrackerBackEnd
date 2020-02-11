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
        Task<UserDTO> Register(string username, string password);

        Task<string> Login(string username, string password);

        Task<bool> UserExists(string username);
        Task<UserDTO> GetUserByIdAsync(string id);
    }
}