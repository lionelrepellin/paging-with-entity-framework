using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging.Buttons
{
    public class First : Button
    {
        public First(int pageNumber, int currentPageNumber, int totalNumberPages, string actionUrl, string parameters) :
            base(pageNumber, currentPageNumber, totalNumberPages, actionUrl, parameters)
        {
            IsSelected = (CurrentPageNumber == 1);
        }

        public override string Draw()
        {
            string navbar = string.Empty;

            if (IsSelected)
                navbar = "<li class='disabled'><a href='#' aria-label='First page'>";
            else
                navbar = string.Format("<li><a href='{0}=1{1}' aria-label='First page'>", ActionUrl, Parameters);

            return string.Concat(navbar, "<span class='glyphicon glyphicon-step-backward' aria-hidden='true'></span></a></li>");
        }
    }
}
