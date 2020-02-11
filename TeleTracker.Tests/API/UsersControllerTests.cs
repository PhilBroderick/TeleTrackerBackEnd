using NUnit.Framework;
using TeleTracker.Controllers;
using TeleTracker.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using TeleTracker.Core.Interfaces;
using Moq;
using System.Threading.Tasks;

namespace TeleTracker.Tests.API
{
    [TestFixture]
    public class UsersControllerTests
    {
        private UsersController _usersController;
        private Mock<IAuthService> _authService;
        private UserDTO _user;

        [SetUp]
        public void SetUp()
        {
            _user = new UserDTO
            {
                Username = "Test",
                ID = "1"
            };
            _authService = new Mock<IAuthService>();
            _authService.Setup(a => a.GetUserByIdAsync("1")).Returns(Task.FromResult(_user));
            _usersController = new UsersController(_authService.Object);
        }

        [Test]
        public void GetUserByIdAsync_IdIsValidUser_ReturnUserDTO()
        {
            var result = (OkObjectResult)_usersController.GetUserByIdAsync("1").Result;

            Assert.AreEqual("1", ((UserDTO)result.Value).ID);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void GetUserByIdAsync_IdIsInvalid_ReturnNotFound(string id)
        {
            var result = (NotFoundResult)_usersController.GetUserByIdAsync(id).Result;

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }
    }
}