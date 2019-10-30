using NUnit.Framework;
using TeleTracker.Controllers;
using Microsoft.AspNetCore.Mvc;
using TeleTracker.DTOs;
using System.Collections.Generic;
using TeleTracker.CustomResponses;

namespace Tests
{
    [TestFixture]
    public class ShowsControllerTests
    {
        private ShowsController _showController;
        private ShowDTO _showDto;

        [SetUp]
        public void Setup()
        {
            _showController = new ShowsController();
            _showDto = new ShowDTO();
            _showDto.ID = "1234";
        }

        [Test]
        public void GetShowByIdAsync_IdIsValid_ReturnShowDto()
        {
            var result = (OkObjectResult)_showController.GetShowByIdAsync("1234");

            Assert.AreEqual(_showDto.ID, ((ShowDTO)result.Value).ID);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void GetShowByIdAsync_IdIsInvalid_ReturnNotFound(string invalidShowID)
        {
            var result = _showController.GetShowByIdAsync(invalidShowID);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }


        [Test]
        public void GetAllShowsAsync_WhenCalled_ReturnsListOfShowDtos()
        {
            var result = (OkObjectResult)_showController.GetAllShowsAsync();
            Assert.That(result.Value, Is.TypeOf<List<ShowDTO>>());
        }

        [Test]
        public void SubscribeToShowAsync_IdIsValid_ReturnSuccessfulSubscribeResponse()
        {
            var result = (OkObjectResult)_showController.SubscribeToShowAsync("1234");

            Assert.That(((SuccessfulSubscribeResponse)result.Value).EntityID, Is.EqualTo("1234"));
        }
    }
}