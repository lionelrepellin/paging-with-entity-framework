using System.Linq;
using NUnit.Framework;
using Moq;
using PagingWithEntityFramework.Business;
using PagingWithEntityFramework.Controllers;
using PagingWithEntityFramework.Domain;
using PagingWithEntityFramework.Models;

namespace PagingWithEntityFramework.Tests
{
    [TestFixture]
    public class HomeControllerTest : BaseTest
    {
        [TestFixtureSetUp]
        public void Initialize()
        {
            // initialize common data
            PageIndex = 0;
            CurrentPage = 1;
            LinesPerPage = 20;
        }

        [Test]
        public void CreateModelTestWithoutCriteria_Ok()
        {
            // Arrange
            var numberOfErrors = Errors.Count();
            var errorContextMock = CreateMockContext();

            // instantiate ErrorService with mock
            var errorService = new ErrorService(errorContextMock.Object);

            // Act
            var controller = new HomeController(errorService);
            var errorModel = controller.CreateModel(new ErrorModel { CurrentPage = CurrentPage }, null);

            // Assert
            Assert.AreEqual(CurrentPage, errorModel.CurrentPage);
            Assert.AreEqual(numberOfErrors, errorModel.Errors.Count());
            Assert.AreEqual(LinesPerPage, errorModel.LinesPerPage);
            Assert.AreEqual(numberOfErrors, errorModel.TotalLines);

            errorContextMock.Verify(c => c.FindAllErrors(), Times.Once());
        }

        [Test]
        public void CreateModelTestWithCriteria_Ok()
        {
            // Arrange
            var searchCriteria = new SearchCriteria
            {
                Severity = "Warning"
            };

            var filteredErrors = Errors.Where(e => e.ErrorLevel.Contains(searchCriteria.Severity)).ToList();
            var numberOfErrors = filteredErrors.Count;

            // Create a mock for ErrorContext
            var errorContextMock = CreateMockContext();            

            // instantiate ErrorService with mock
            var errorService = new ErrorService(errorContextMock.Object);

            var homeController = new HomeController(errorService);
            var errorModel = new ErrorModel
            {
                CurrentPage = CurrentPage,
                ErrorLevel = "Warning"
            };

            // Act
            var model = homeController.CreateModel(errorModel, searchCriteria);

            // Assert
            Assert.AreEqual(CurrentPage, errorModel.CurrentPage);
            Assert.AreEqual(numberOfErrors, errorModel.Errors.Count());
            Assert.AreEqual(LinesPerPage, errorModel.LinesPerPage);
            Assert.AreEqual(numberOfErrors, errorModel.TotalLines);

            errorContextMock.Verify(c => c.FindAllErrors(), Times.Once());            
        }
    }
}
