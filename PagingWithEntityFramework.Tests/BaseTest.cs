using Moq;
using PagingWithEntityFramework.DAL;
using PagingWithEntityFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagingWithEntityFramework.Tests
{
    public abstract class BaseTest
    {
        protected int pageIndex;
        protected int currentPage;
        protected int linesPerPage;

        protected IEnumerable<Error> errors = new List<Error>
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

        /// <summary>
        /// Create a mock for ErrorContext
        /// </summary>
        /// <returns></returns>
        protected Mock<ErrorContext> CreateMockContext()
        {
            var errorContextMock = new Mock<ErrorContext>();
            var queryableErrors = errors.Select(e => e).AsQueryable<Error>();
            errorContextMock.Setup(c => c.FindAllErrors()).Returns(queryableErrors);
            return errorContextMock;
        }
    }
}
