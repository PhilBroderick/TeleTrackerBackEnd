using NUnit.Framework;
using TeleTracker.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TeleTracker.CustomResponses;
using TeleTracker.Core.DTOs;
using TeleTracker.Core.Interfaces;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TeleTracker.Tests.API
{
    [TestFixture]
    public class ShowsControllerTests
    {
        private ShowsController _showController;
        private ShowDTO _showDto;
        private ShowSubscriptionDTO _showSubDto;
        private Mock<IShowService> _showService;
        private Mock<ISubscriptionService> _subService;
        private Mock<HttpContext> _contextMock;
        private static readonly string _userId = "123";

        [SetUp]
        public void Setup()
        {
            _showDto = new ShowDTO
            {
                ID = "1234"
            };
            _showSubDto = new ShowSubscriptionDTO
            {
                Id = _showDto.ID,
                Title = "Test"
            };
            _showService = new Mock<IShowService>();
            _showService.Setup(s => s.GetMostPopularShows()).Returns(Task.FromResult(new List<ShowPopularityDTO>()));
            _showService.Setup(s => s.GetShowByIdAsync(1234)).Returns(Task.FromResult(_showDto));
            _subService = new Mock<ISubscriptionService>();
            _subService.Setup(s => s.SubscribeToShow(_showSubDto, "123")).Returns(Task.FromResult(true));
            _contextMock = new Mock<HttpContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, _userId),
            }, "mock"));
            _contextMock.Setup(x => x.User).Returns(user);
            _showController = new ShowsController(_showService.Object, _subService.Object);
            _showController.ControllerContext.HttpContext = _contextMock.Object;
        }

        [Test]
        public void GetShowByIdAsync_IdIsValid_ReturnShowDto()
        {
            var result = (OkObjectResult)_showController.GetShowByIdAsync(1234).Result;

            Assert.AreEqual(_showDto.ID, ((ShowDTO)result.Value).ID);
        }

        [Test]
        [TestCase(null)]
        [TestCase(0)]
        [TestCase(-1)]
        public void GetShowByIdAsync_IdIsInvalid_ReturnNotFound(int invalidShowID)
        {
            var result = _showController.GetShowByIdAsync(invalidShowID).Result;
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
            var result = (OkObjectResult)_showController.SubscribeToShowAsync(_showDto.ID).Result;

            Assert.That(((SubscribeToShowResponse)result.Value).Message, Does.Contain("successfully"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SubscribeToShowAsync_IdIsInvalid_ReturnUnsuccesfulSubscribeResponse(string invalidShowID)
        {
            var result = (NotFoundObjectResult)_showController.SubscribeToShowAsync(invalidShowID).Result;

            Assert.That(((SubscribeToShowResponse)result.Value).Message, Does.Contain("unsuccessfully"));
        }

        [Test]
        public void GetPopularShowsAsync_WhenCalled_ReturnsListOfPopularShows()
        {
            var result = (OkObjectResult)_showController.GetPopularShowsAsync().Result;

            Assert.That(result.Value, Is.TypeOf<List<ShowPopularityDTO>>());
        }
    }
}