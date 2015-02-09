using PagingWithEntityFramework.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagingWithEntityFramework.Domain;


namespace PagingWithEntityFramework.Business
{
    public class ErrorService
    {
        private ErrorContext _errorContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorContext">ErrorContext injected by Unity</param>
        public ErrorService(ErrorContext errorContext)
        {
            if (errorContext == null)
                throw new ArgumentNullException("errorContext");

            _errorContext = errorContext;
        }

        public ErrorResult RetrieveErrors(int page, int linesPerPage, SearchCriteria criteria)
        {
            var pageIndex = page - 1;

            // prevent index out of bounds exception
            if (pageIndex < 0) pageIndex = 0;

            if (criteria == null)
            {
                return new ErrorResult
                {
                    TotalLines = _errorContext.FindTotalNumberOfErrors(),
                    Errors = _errorContext.FindErrorsByPageIndex(pageIndex, linesPerPage)
                };
            }
            else
            {
                return new ErrorResult
                {
                    TotalLines = _errorContext.FindTotalNumberOfErrorsWithCriteria(criteria),
                    Errors = _errorContext.FindErrorsByPageIndexAndCriteria(pageIndex, linesPerPage, criteria)
                };
            }
        }

        /// <summary>
        /// Return a SearchCriteria object if search criteria have been defined
        /// </summary>
        /// <returns></returns>
        public SearchCriteria GetDefinedSearchCriteria(string serverName, string errorLevel, string errorMessage)
        {
            if (string.IsNullOrEmpty(serverName) && string.IsNullOrEmpty(errorLevel) && string.IsNullOrEmpty(errorMessage))
            {
                return null;
            }
            else
            {
                return new SearchCriteria
                {
                    ServerName = serverName,
                    Severity = errorLevel,
                    StackTrace = errorMessage
                };
            }
        }
    }
}
