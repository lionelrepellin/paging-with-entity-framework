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
        private int _pageIndex;
        private int _currentPage;
        private int _linesPerPage;
        private IEnumerable<Error> _errors;

        [TestInitialize]
        public void Initialize()
        {
            // initialize common data
            _pageIndex = 0;
            _currentPage = 1;
            _linesPerPage = 20;
            _errors = GetErrors();
        }

        [TestMethod]
        public void CreateModelTestWithoutCriteria_Ok()
        {
            // Arrange
            var numberOfErrors = _errors.Count();

            // Create a mock for ErrorContext
            var errorContextMock = new Mock<ErrorContext>();
            errorContextMock.Setup(c => c.FindTotalNumberOfErrors()).Returns(numberOfErrors);
            errorContextMock.Setup(c => c.FindErrorsByPageIndex(_pageIndex, _linesPerPage)).Returns(_errors);

            // instanciate ErrorService with mock
            var errorService = new ErrorService(errorContextMock.Object);

            // Act
            var controller = new HomeController(errorService);
            var errorModel = controller.CreateModel(new Models.ErrorModel { CurrentPage = _currentPage });

            // Assert
            Assert.AreEqual(_currentPage, errorModel.CurrentPage);
            Assert.AreEqual(numberOfErrors, errorModel.Errors.Count());
            Assert.AreEqual(_linesPerPage, errorModel.LinesPerPage);
            Assert.AreEqual(numberOfErrors, errorModel.TotalLines);
            
            errorContextMock.Verify(c => c.FindTotalNumberOfErrors(), Times.Once());
            errorContextMock.Verify(c => c.FindErrorsByPageIndex(_pageIndex, _linesPerPage), Times.Once());
        }
    }
}
