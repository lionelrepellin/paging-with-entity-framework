using PagingWithEntityFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PagingWithEntityFramework.Models
{
    public class ErrorModel
    {
        #region Data bound to the form

        [Display(Name = "Server name")]
        public string Name { get; set; }

        [Display(Name = "Severity")]
        public string ErrorLevel { get; set; }

        [Display(Name = "Message")]
        public string StackTrace { get; set; }

        // hidden field in the view
        public int CurrentPage { get; set; }

        #endregion

        public int LinesPerPage { get; set; }
        public IEnumerable<Error> Errors { get; set; }
        public int TotalLines { get; set; }

        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling(TotalLines * 1.0 / LinesPerPage);
            }
        }

        public ErrorModel()
        {
        }

        public string GetQueryParameters()
        {
            if (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(StackTrace))
                return string.Empty;
            else
                return string.Format("Name={0}&ErrorLevel={1}&StackTrace={2}", Name, ErrorLevel, StackTrace);
        }
    }
}