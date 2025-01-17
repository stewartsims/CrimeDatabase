using CrimeDatabase.Controllers;
using CrimeDatabase.Data;
using CrimeDatabase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace CrimeDatabaseTests
{
    // some basic unit tests -
    // would suggest as more logic is added controllers are more thoroughly tested
    // and integration tests introduced for testing end-to-end repository behaviour
    public class CrimeEventsControllerTests
    {
        private readonly Mock<ICrimeEventRepository> _mockRepo;
        private readonly CrimeEventsController _controller;

        public CrimeEventsControllerTests()
        {
            _mockRepo = new Mock<ICrimeEventRepository>();
            _controller = new CrimeEventsController(_mockRepo.Object) { TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>()) };
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _controller.Index(null);
            Assert.IsType<ViewResult>(result.Result);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsExactNumberOfCrimeEvents_MultipleResults()
        {
            _mockRepo.Setup(repo => repo.Search(null))
                    .Returns(new List<CrimeEvent>() { 
                        new CrimeEvent() { Id = 1, CrimeType = CrimeDatabase.CrimeTypeEnum.CriminalDamage, 
                            LocationArea = "Test Area", LocationTown = "Test Town", VictimName = "A. Smith" }, new CrimeEvent() { Id = 1, CrimeType = CrimeDatabase.CrimeTypeEnum.CriminalDamage, LocationArea = "Test Area", LocationTown = "Test Town", VictimName = "J. Bloggs" } 
                    });
            var result = _controller.Index(null);
            var viewResult = result.Result as ViewResult;
            var crimeEvents = Assert.IsType<List<CrimeEvent>>(viewResult.Model);
            Assert.Equal(2, crimeEvents.Count);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsSingleResult_WithSearchString()
        {
            _mockRepo.Setup(repo => repo.Search("Bloggs"))
                    .Returns(new List<CrimeEvent>() { 
                        new CrimeEvent() { Id = 1, CrimeType = CrimeDatabase.CrimeTypeEnum.CriminalDamage, LocationArea = "Test Area", LocationTown = "Test Town", VictimName = "A. Smith" }, 
                    });
            var result = _controller.Index("Bloggs");
            var viewResult = result.Result as ViewResult;
            var crimeEvents = Assert.IsType<List<CrimeEvent>>(viewResult.Model);
            Assert.Single(crimeEvents);
        }
    }
}