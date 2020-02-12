using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeleTracker.Controllers;
using TeleTracker.Core.DTOs;
using TeleTracker.Core.Interfaces;

namespace TeleTracker.Tests.API
{
    [TestFixture]
    public class MoviesControllerTests
    {
        private Mock<IMovieService> _movieService;
        private MoviesController _controller;

        [SetUp]
        public void Setup()
        {
            _movieService = new Mock<IMovieService>();
            _movieService.Setup(m => m.GetMovieByIdAsync("1")).Returns(Task.FromResult(new MovieDTO { ID = "1" }));
            _controller = new MoviesController(_movieService.Object);
        }

        [Test]
        public void GetMovieByIdAsync_ValidId_ReturnsMovieDTOWithCorrectID()
        {
            var result = (OkObjectResult)_controller.GetMovieByIdAsync("1").Result;

            Assert.AreEqual("1", ((MovieDTO)result.Value).ID);
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GetMovieByIdAsync_InvalidId_ReturnsNotFound(string id)
        {
            var result = _controller.GetMovieByIdAsync(id).Result;

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }
    }
}
