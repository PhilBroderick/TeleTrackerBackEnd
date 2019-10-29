using NUnit.Framework;
using TeleTracker.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
    [TestFixture]
    public class ShowsControllerTests
    {
        private ShowsController _showController;

        [SetUp]
        public void Setup()
        {
            _showController = new ShowsController();
        }

        [Test]
        public void GetShow_IdIsNotZero_ReturnOk()
        {
            var result = _showController.GetShowByIdAsync("1");
            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void GetShow_IdIsInvalid_ReturnNotFound(string invalidShowID)
        {
            var result = _showController.GetShowByIdAsync(invalidShowID);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void GetShow_IdIsValid_ReturnId()
        {
            var result = (OkObjectResult)_showController.GetShowByIdAsync("1");

            Assert.AreEqual("1", result.Value);
        }
    }
}