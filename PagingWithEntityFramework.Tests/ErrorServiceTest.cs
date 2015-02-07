using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PagingWithEntityFramework.Business;
using PagingWithEntityFramework.DAL;
using PagingWithEntityFramework.Domain;
using PagingWithEntityFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PagingWithEntityFramework.Tests
{
    [TestClass]
    public class ErrorServiceTest : BaseTest
    {
        private int _page;
        private int _pageIndex;
        private int _linesPerPage;
        private IEnumerable<Error> _errors;

        [TestInitialize]
        public void Initialize()
        {
            // initialize common data
            _page = 1;
            _pageIndex = 0;
            _linesPerPage = 30;
            _errors = GetErrors();
        }

        [TestMethod]
        public void RetrieveErrorsTestWithoutCriteria_Ok()
        {
            // Arrange
            var numberOfErrors = _errors.Count();

            // Create a mock for ErrorContext
            var errorContextMock = new Mock<ErrorContext>();
            errorContextMock.Setup(c => c.FindTotalNumberOfErrors()).Returns(numberOfErrors);
            errorContextMock.Setup(c => c.FindErrorsByPageIndex(_pageIndex, _linesPerPage)).Returns(_errors);
            
            // Act
            var errorService = new ErrorService(errorContextMock.Object);
            var errorResult = errorService.RetrieveErrors(_page, _linesPerPage, null);

            // Assert
            Assert.AreEqual(numberOfErrors, errorResult.TotalLines);
            Assert.AreEqual(numberOfErrors, errorResult.Errors.Count());

            errorContextMock.Verify(c => c.FindTotalNumberOfErrors(), Times.Once());
            errorContextMock.Verify(c => c.FindErrorsByPageIndex(_pageIndex, _linesPerPage), Times.Once());
        }

        [TestMethod]
        public void RetrieveErrorsTestWithCriteria_Ok()
        {
            // Arrange
            var searchCriteria = new SearchCriteria
            {
                ServerName = "Server_1"
            };

            var filteredErrors = _errors.Where(e => e.ServerName.Contains(searchCriteria.ServerName)).ToList();

            // Create a mock for ErrorContext
            var errorContextMock = new Mock<ErrorContext>();
            errorContextMock.Setup(c => c.FindTotalNumberOfErrorsWithCriteria(searchCriteria)).Returns(filteredErrors.Count());
            errorContextMock.Setup(c => c.FindErrorsByPageIndexAndCriteria(_pageIndex, _linesPerPage, searchCriteria)).Returns(filteredErrors);

            // Act
            var errorService = new ErrorService(errorContextMock.Object);
            var errorResult = errorService.RetrieveErrors(_page, _linesPerPage, searchCriteria);

            // Assert
            Assert.AreEqual(filteredErrors.Count(), errorResult.TotalLines);
            Assert.AreEqual(filteredErrors.Count(), errorResult.Errors.Count());

            errorContextMock.Verify(c => c.FindTotalNumberOfErrorsWithCriteria(searchCriteria), Times.Once());
            errorContextMock.Verify(c => c.FindErrorsByPageIndexAndCriteria(_pageIndex, _linesPerPage, searchCriteria), Times.Once());
        }
    }
}
