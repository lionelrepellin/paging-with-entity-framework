using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging.Buttons
{
    public abstract class Button
    {
        public int PageNumber { get; private set; }
        public int CurrentPageNumber { get; protected set; }
        public int TotalNumberPages { get; private set; }
        public string ActionUrl { get; private set; }
        public string Parameters { get; private set; }
        public bool IsSelected { get; protected set; }

        public Button(int pageNumber, int currentPageNumber, int totalNumberPages, string actionUrl, string parameters)
        {
            PageNumber = pageNumber;
            CurrentPageNumber = currentPageNumber;
            TotalNumberPages = totalNumberPages;
            ActionUrl = actionUrl;
            Parameters = parameters;            
        }

        public abstract string Draw();
    }
}
