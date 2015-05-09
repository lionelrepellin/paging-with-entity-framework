using System.Linq;
using DeepEqual.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PagingWithEntityFramework.Business;
using PagingWithEntityFramework.Domain;

namespace PagingWithEntityFramework.Tests
{
    [TestClass]
    public class ErrorServiceTest : BaseTest
    {                
        [TestInitialize]
        public void Initialize()
        {
            // initialize common data
            CurrentPage = 1;
            PageIndex = 0;
            LinesPerPage = 30;            
        }

        [TestMethod]
        public void RetrieveErrorsTestWithoutCriteria_Ok()
        {
            // Arrange            
            var errorContextMock = CreateMockContext();

            // Act
            var errorService = new ErrorService(errorContextMock.Object);
            var errorResult = errorService.RetrieveErrors(CurrentPage, LinesPerPage);
            
            // expected result with errors sorted by id descending
            var expectedErrorResult = new ErrorResult
            {
                Errors = Errors.OrderByDescending(e => e.Id),
                TotalLines = Errors.Count()
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
            const int page = -1;
            var errorContextMock = CreateMockContext();

            // Act
            var errorService = new ErrorService(errorContextMock.Object);
            var errorResult = errorService.RetrieveErrors(page, LinesPerPage);

            // expected result with errors sorted by id descending
            var expectedErrorResult = new ErrorResult
            {
                Errors = Errors.OrderByDescending(e => e.Id),
                TotalLines = Errors.Count()
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

            var filteredErrors = Errors.Where(e => e.ServerName.Contains(searchCriteria.ServerName)).OrderByDescending(e => e.Id);

            var expectedErrorResult = new ErrorResult
            {
                Errors = filteredErrors,
                TotalLines = filteredErrors.Count()
            };
            
            var errorContextMock = CreateMockContext();

            // Act
            var errorService = new ErrorService(errorContextMock.Object);
            var errorResult = errorService.RetrieveErrors(CurrentPage, LinesPerPage, searchCriteria);

            // Assert
            var result = expectedErrorResult.IsDeepEqual(errorResult);
            Assert.IsTrue(result);

            errorContextMock.Verify(c => c.FindAllErrors(), Times.Once());
        }
    }
}
