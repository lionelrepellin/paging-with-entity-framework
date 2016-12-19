using System;
using System.Linq;
using DeepEqual.Syntax;
using Moq;
using NUnit.Framework;
using PagingWithEntityFramework.Business;
using PagingWithEntityFramework.Domain;

namespace PagingWithEntityFramework.Tests
{
    [TestFixture]
    public class ErrorServiceTest : BaseTest
    {                
        [TestFixtureSetUp]
        public void Initialize()
        {
            // initialize common data
            CurrentPage = 1;
            PageIndex = 0;
            LinesPerPage = 30;            
        }

        [Test]
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

        [Test]
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

        [Test]
        public void RetrieveErrorsTestWithCriteria_Ok()
        {
            // Arrange
            var searchCriteria = new SearchCriteria
            {
                ServerName = "Server_1",
                StackTrace = ""
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

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentNullExceptionIfNoContext()
        {
            var errorService = new ErrorService(null);
        }
    }
}
