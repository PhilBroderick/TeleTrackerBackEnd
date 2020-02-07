using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using TeleTracker.BLL;
using TeleTracker.Core.DTOs;
using TeleTracker.Core.Interfaces;
using TeleTracker.Core.Models;

namespace TeleTracker.Tests.BLL
{
    [TestFixture]
    public class UserAuthenticationTests
    {
        private Mock<IAuthRepository> _mockRepo;
        private Mock<IConfiguration> _config;
        private User _userInput;
        private static readonly string _password = "testPassword";
        private User _userOutput;
        private UserAuthenticationService _authService;
        private static readonly string _usernameThatDoesExist = "userThatExists";
        private static readonly string _usernameThatDoesntExist = "userThatDoesntExist";

        [SetUp]
        public void SetUp()
        {
            _userInput = new User
            {
                Username = "test"
            };
            _userOutput = new User
            {
                ID = 1,
                Username = "test",
                PasswordHash = new byte[1],
                PasswordSalt = new byte[1]
            };
            _mockRepo = new Mock<IAuthRepository>();
            _mockRepo.Setup(c => c.Register(
                It.Is<User>(c => c.Username == _userInput.Username), _password))
                .Returns(Task.FromResult(_userOutput));
            _mockRepo.Setup(c => c.Register(
                It.Is<User>(c => c.Username != _userInput.Username), It.Is<string>(c => !c.Contains(_password))))
                .Returns(Task.FromResult<User>(null));
            _mockRepo.Setup(c => c.UserExists(_usernameThatDoesExist.ToLower())).Returns(Task.FromResult(true));
            _mockRepo.Setup(c => c.UserExists(_usernameThatDoesntExist.ToLower())).Returns(Task.FromResult(false));
            _config = new Mock<IConfiguration>();
            _authService = new UserAuthenticationService(_mockRepo.Object, _config.Object);
        }

        [Test]
        public void Login_ValidUserNameAndPassword_ReturnsValidJWTToken()
        {
        }

        [Test]
        public void Register_ValidUserAndPassword_ReturnsNewlyCreatedUser()
        {
            var result = _authService.Register(_userInput.Username, _password).Result;

            Assert.IsInstanceOf(typeof(UserDTO), result);
            Assert.AreEqual(_userOutput.ID.ToString(), result.ID);
        }

        [Test]
        [TestCase("", "")]
        [TestCase(" ", " ")]
        [TestCase(null, null)]
        public void Register_InvalidUserOrPassword_ReturnsNull(string username, string password)
        {
            var result = _authService.Register(username, password).Result;

            Assert.IsNull(result);
        }

        [Test]
        public void UserExists_UsernameThatDoesExists_ReturnsTrue()
        {
            var result = _authService.UserExists(_usernameThatDoesExist).Result;

            Assert.IsTrue(result);
        }

        [Test]
        public void UserExists_UsernameThatDoesntExists_ReturnsFalse()
        {
            var result = _authService.UserExists(_usernameThatDoesntExist).Result;

            Assert.IsFalse(result);
        }
    }
}