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
        public static ErrorResult RetrieveErrors(int page, int linesPerPage, SearchCriteria criteria)
        {
            using (var ctx = new Context())
            {
                var pageIndex = page - 1;

                // prevent index out of bounds exception
                if (pageIndex < 0) pageIndex = 0;

                if (criteria == null)
                {
                    return new ErrorResult
                    {
                        TotalLines = ctx.FindTotalNumberOfErrors(),
                        Errors = ctx.FindErrorsByPageIndex(pageIndex, linesPerPage)
                    };
                }
                else
                {
                    return new ErrorResult
                    {
                        TotalLines = ctx.FindTotalNumberOfErrorsWithCriteria(criteria),
                        Errors = ctx.FindErrorsByPageIndexAndCriteria(pageIndex, linesPerPage, criteria)
                    };
                }
            }
        }

    }
}
