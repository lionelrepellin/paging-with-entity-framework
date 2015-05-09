using System;
using System.Linq;
using PagingWithEntityFramework.DAL;
using PagingWithEntityFramework.Domain;

namespace PagingWithEntityFramework.Business
{
    public class ErrorService
    {
        private readonly ErrorContext _errorContext;

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

        /// <summary>
        /// Retrieve data from database and filter them with search criteria
        /// </summary>
        /// <param name="page"></param>
        /// <param name="linesPerPage"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        public ErrorResult RetrieveErrors(int page, int linesPerPage, SearchCriteria searchCriteria = null)
        {
            var pageIndex = page - 1;

            // prevent index out of bounds exception
            if (pageIndex < 0) pageIndex = 0;

            var query = _errorContext.FindAllErrors();

            if (searchCriteria != null)
            {
                if (!string.IsNullOrEmpty(searchCriteria.StackTrace))
                    query = query.Where(e => e.Stacktrace.Contains(searchCriteria.StackTrace));

                if (!string.IsNullOrEmpty(searchCriteria.ServerName))
                    query = query.Where(e => e.ServerName.Contains(searchCriteria.ServerName));

                if (!string.IsNullOrEmpty(searchCriteria.Severity))
                    query = query.Where(e => e.ErrorLevel.Contains(searchCriteria.Severity));
            }

            query = query.OrderByDescending(e => e.Id);

            return new ErrorResult
            {
                Errors = query.Skip(pageIndex * linesPerPage).Take(linesPerPage).ToList(),
                TotalLines = query.Count()
            };
        }
    }
}
