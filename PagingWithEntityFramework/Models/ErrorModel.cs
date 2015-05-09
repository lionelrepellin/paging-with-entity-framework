using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PagingWithEntityFramework.Domain.Entities;

namespace PagingWithEntityFramework.Models
{
    public class ErrorModel
    {
        #region Data bound to the form

        [Display(Name = "Server name")]
        public string Name { get; set; }

        [Display(Name = "Severity")]
        public string ErrorLevel { get; set; }

        [Display(Name = "Error Message")]
        public string ErrorMessage { get; set; }

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
            if (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(ErrorLevel) && string.IsNullOrEmpty(ErrorMessage))
                return string.Empty;
            else
                return string.Format("Name={0}&ErrorLevel={1}&ErrorMessage={2}", Name, ErrorLevel, ErrorMessage);
        }
    }
}