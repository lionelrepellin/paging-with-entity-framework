using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PagingWithEntityFramework.Business;
using PagingWithEntityFramework.DAL;
using PagingWithEntityFramework.Domain;
using PagingWithEntityFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using DeepEqual.Syntax;

namespace PagingWithEntityFramework.Tests
{
    [TestClass]
    public class ErrorServiceTest : BaseTest
    {                
        [TestInitialize]
        public void Initialize()
        {
            // initialize common data
            currentPage = 1;
            pageIndex = 0;
            linesPerPage = 30;            
        }

        [TestMethod]
        public void RetrieveErrorsTestWithoutCriteria_Ok()
        {
            // Arrange            
            var errorContextMock = CreateMockContext();

            // Act
            var errorService = new ErrorService(errorContextMock.Object);
            var errorResult = errorService.RetrieveErrors(currentPage, linesPerPage);
            
            // expected result with errors sorted by id descending
            var expectedErrorResult = new ErrorResult
            {
                Errors = errors.OrderByDescending(e => e.Id),
                TotalLines = errors.Count()
            };

            // Assert
            var result = expectedErrorResult.IsDeepEqual(errorResult);
            Assert.IsTrue(result);

            errorContextMock.Verify(c => c.FindAllErrors(), Times.Once());
        }

        [TestMethod]
        public void RetrieveErrorsTestWithoutCriteriaAndNegativePage_Ok()
        {
            // Arrange            
            var page = -1;
            var errorContextMock = CreateMockContext();

            // Act
            var errorService = new ErrorService(errorContextMock.Object);
            var errorResult = errorService.RetrieveErrors(page, linesPerPage);

            // expected result with errors sorted by id descending
            var expectedErrorResult = new ErrorResult
            {
                Errors = errors.OrderByDescending(e => e.Id),
                TotalLines = errors.Count()
            };

            // Assert
            var result = expectedErrorResult.IsDeepEqual(errorResult);
            Assert.IsTrue(result);

            errorContextMock.Verify(c => c.FindAllErrors(), Times.Once());
        }

        [TestMethod]
        public void RetrieveErrorsTestWithCriteria_Ok()
        {
            // Arrange
            var searchCriteria = new SearchCriteria
            {
                ServerName = "Server_1"
            };

            var filteredErrors = errors.Where(e => e.ServerName.Contains(searchCriteria.ServerName)).OrderByDescending(e => e.Id);

            var expectedErrorResult = new ErrorResult
            {
                Errors = filteredErrors,
                TotalLines = filteredErrors.Count()
            };
            
            var errorContextMock = CreateMockContext();

            // Act
            var errorService = new ErrorService(errorContextMock.Object);
            var errorResult = errorService.RetrieveErrors(currentPage, linesPerPage, searchCriteria);

            // Assert
            var result = expectedErrorResult.IsDeepEqual(errorResult);
            Assert.IsTrue(result);

            errorContextMock.Verify(c => c.FindAllErrors(), Times.Once());
        }
    }
}
