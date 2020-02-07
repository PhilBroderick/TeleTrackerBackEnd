using NUnit.Framework;
using TeleTracker.Controllers;
using TeleTracker.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace TeleTracker.Tests.API
{
    [TestFixture]
    public class UsersControllerTests
    {
        private UsersController _usersController;

        [SetUp]
        public void SetUp()
        {
            _usersController = new UsersController();
        }

        [Test]
        public void GetUserByIdAsync_IdIsValidUser_ReturnUserDTO()
        {
            var result = (OkObjectResult)_usersController.GetUserByIdAsync("1");

            Assert.AreEqual("1", ((UserDTO)result.Value).ID);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void GetUserByIdAsync_IdIsInvalid_ReturnNotFound(string id)
        {
            var result = (NotFoundResult)_usersController.GetUserByIdAsync(id);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }
    }
}