using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging.Buttons
{
    public class Next : Button
    {
        public Next(int pageNumber, int currentPageNumber, int totalNumberPages, string actionUrl, string parameters) :
            base(pageNumber, currentPageNumber, totalNumberPages, actionUrl, parameters)
        {
            IsSelected = (CurrentPageNumber == TotalNumberPages);
        }

        public override string Draw()
        {
            string navbar = string.Empty;

            if (IsSelected)
                navbar = "<li class='disabled'><a href='#' aria-label='Next'>";
            else
                navbar = string.Format("<li><a href='{0}={1}{2}' aria-label='Next'>", ActionUrl, CurrentPageNumber + 1, Parameters);

            return string.Concat(navbar, "<span class='glyphicon glyphicon-chevron-right' aria-hidden='true'></span></a></li>");
        }
    }
}
