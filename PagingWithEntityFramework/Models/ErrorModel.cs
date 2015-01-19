using PagingWithEntityFramework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PagingWithEntityFramework.Models
{
    public class ErrorModel
    {
        private readonly int linesPerPage;

        public IEnumerable<Error> Errors { get; private set; }

        public int CurrentPage { get; private set; }

        public int TotalLines { get; private set; }

        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling(TotalLines * 1.0 / linesPerPage);    
            }
        }

        public ErrorModel(IEnumerable<Error> errors, int linesPerPage, int currentPage, int totalLines)
        {
            this.Errors = errors;
            this.linesPerPage = linesPerPage;
            this.CurrentPage = currentPage;
            this.TotalLines = totalLines;
        }
    }
}