using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging.Buttons
{
    public class Page : Button
    {
        public Page(int pageNumber, int currentPageNumber, int totalNumberPages, string actionUrl, string parameters) :
            base(pageNumber, currentPageNumber, totalNumberPages, actionUrl, parameters)
        {
            IsSelected = (PageNumber == CurrentPageNumber);
        }

        public override string Draw()
        {
            if (IsSelected)
                return string.Format("<li class='active'><a href='#'>{0}</a></li>", CurrentPageNumber);
            else
                return string.Format("<li><a href='{0}={1}{2}'>{1}</a></li>", ActionUrl, PageNumber, Parameters);
        }
    }
}
