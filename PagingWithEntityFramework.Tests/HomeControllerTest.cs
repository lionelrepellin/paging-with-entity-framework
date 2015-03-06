using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PagingWithEntityFramework.Business;
using PagingWithEntityFramework.Controllers;
using PagingWithEntityFramework.DAL;
using PagingWithEntityFramework.Domain;
using PagingWithEntityFramework.Domain.Entities;
using PagingWithEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PagingWithEntityFramework.Tests
{
    [TestClass]
    public class HomeControllerTest : BaseTest
    {
        [TestInitialize]
        public void Initialize()
        {
            // initialize common data
            pageIndex = 0;
            currentPage = 1;
            linesPerPage = 20;
        }

        [TestMethod]
        public void CreateModelTestWithoutCriteria_Ok()
        {
            // Arrange
            var numberOfErrors = errors.Count();
            var errorContextMock = CreateMockContext();

            // instantiate ErrorService with mock
            var errorService = new ErrorService(errorContextMock.Object);

            // Act
            var controller = new HomeController(errorService);
            var errorModel = controller.CreateModel(new Models.ErrorModel { CurrentPage = currentPage }, null);

            // Assert
            Assert.AreEqual(currentPage, errorModel.CurrentPage);
            Assert.AreEqual(numberOfErrors, errorModel.Errors.Count());
            Assert.AreEqual(linesPerPage, errorModel.LinesPerPage);
            Assert.AreEqual(numberOfErrors, errorModel.TotalLines);

            errorContextMock.Verify(c => c.FindAllErrors(), Times.Once());
        }

        [TestMethod]
        public void CreateModelTestWithCriteria_Ok()
        {
            // Arrange
            var searchCriteria = new SearchCriteria
            {
                Severity = "Warning"
            };

            var filteredErrors = errors.Where(e => e.ErrorLevel.Contains(searchCriteria.Severity)).ToList();
            var numberOfErrors = filteredErrors.Count;

            // Create a mock for ErrorContext
            var errorContextMock = CreateMockContext();            

            // instantiate ErrorService with mock
            var errorService = new ErrorService(errorContextMock.Object);

            var homeController = new HomeController(errorService);
            var errorModel = new ErrorModel
            {
                CurrentPage = currentPage,
                ErrorLevel = "Warning"
            };

            // Act
            var model = homeController.CreateModel(errorModel, searchCriteria);

            // Assert
            Assert.AreEqual(currentPage, errorModel.CurrentPage);
            Assert.AreEqual(numberOfErrors, errorModel.Errors.Count());
            Assert.AreEqual(linesPerPage, errorModel.LinesPerPage);
            Assert.AreEqual(numberOfErrors, errorModel.TotalLines);

            errorContextMock.Verify(c => c.FindAllErrors(), Times.Once());            
        }
    }
}
