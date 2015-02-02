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
    public class ErrorServiceTest
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

            var mockContext = new Mock<ErrorContext>();
            mockContext.Setup(c => c.FindTotalNumberOfErrors()).Returns(numberOfErrors);
            mockContext.Setup(c => c.FindErrorsByPageIndex(_pageIndex, _linesPerPage)).Returns(_errors);
            
            // Act
            var errorService = new ErrorService(mockContext.Object);
            var errorResult = errorService.RetrieveErrors(_page, _linesPerPage, null);

            // Assert
            Assert.AreEqual(numberOfErrors, errorResult.TotalLines);
            Assert.AreEqual(numberOfErrors, errorResult.Errors.Count());

            mockContext.Verify(c => c.FindTotalNumberOfErrors(), Times.Once());
            mockContext.Verify(c => c.FindErrorsByPageIndex(_pageIndex, _linesPerPage), Times.Once());
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

            var mockContext = new Mock<ErrorContext>();
            mockContext.Setup(c => c.FindTotalNumberOfErrorsWithCriteria(searchCriteria)).Returns(filteredErrors.Count());
            mockContext.Setup(c => c.FindErrorsByPageIndexAndCriteria(_pageIndex, _linesPerPage, searchCriteria)).Returns(filteredErrors);

            // Act
            var errorService = new ErrorService(mockContext.Object);
            var errorResult = errorService.RetrieveErrors(_page, _linesPerPage, searchCriteria);

            // Assert
            Assert.AreEqual(filteredErrors.Count(), errorResult.TotalLines);
            Assert.AreEqual(filteredErrors.Count(), errorResult.Errors.Count());

            mockContext.Verify(c => c.FindTotalNumberOfErrorsWithCriteria(searchCriteria), Times.Once());
            mockContext.Verify(c => c.FindErrorsByPageIndexAndCriteria(_pageIndex, _linesPerPage, searchCriteria), Times.Once());
        }

        private IEnumerable<Error> GetErrors()
        {
            return new List<Error>
            {
                new Error { Id = 1, CurrentDate = DateTime.Now.AddMinutes(1), ServerName = "Server_1", ErrorLevel = "Warning", Stacktrace = "" },
                new Error { Id = 2, CurrentDate = DateTime.Now.AddMinutes(2), ServerName = "Server_1", ErrorLevel = "Error", Stacktrace = "" },
                new Error { Id = 3, CurrentDate = DateTime.Now.AddMinutes(3), ServerName = "Server_3", ErrorLevel = "Fatal", Stacktrace = "" },
                new Error { Id = 4, CurrentDate = DateTime.Now.AddMinutes(4), ServerName = "Server_2", ErrorLevel = "Warning", Stacktrace = "" },
                new Error { Id = 5, CurrentDate = DateTime.Now.AddMinutes(5), ServerName = "Server_4", ErrorLevel = "Error", Stacktrace = "" },
                new Error { Id = 6, CurrentDate = DateTime.Now.AddMinutes(6), ServerName = "Server_1", ErrorLevel = "Fatal", Stacktrace = "" },
                new Error { Id = 7, CurrentDate = DateTime.Now.AddMinutes(7), ServerName = "Server_3", ErrorLevel = "Warning", Stacktrace = "" },
                new Error { Id = 8, CurrentDate = DateTime.Now.AddMinutes(8), ServerName = "Server_2", ErrorLevel = "Error", Stacktrace = "" },
                new Error { Id = 9, CurrentDate = DateTime.Now.AddMinutes(9), ServerName = "Server_1", ErrorLevel = "Fatal", Stacktrace = "" },
                new Error { Id = 10, CurrentDate = DateTime.Now.AddMinutes(10), ServerName = "Server_3", ErrorLevel = "Warning", Stacktrace = "" }
            };
        }
    }
}
