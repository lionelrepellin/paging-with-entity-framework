using Paging.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paging
{
    /// <summary>
    /// Create a navigation bar
    /// </summary>
    public class NavBar
    {
        private const int MAX_PAGE_TO_DISPLAY = 11;
        private const int PAGES_AROUND_CURRENT = 5;

        private bool _showFirstLastButtons;
        private bool _showPreviousNextButtons;

        protected int CurrentPage;
        protected int TotalPages;
        protected string ActionUrl;
        protected string QueryParameters = string.Empty;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="currentPage">Page number selected</param>
        /// <param name="totalPages">Total number of pages</param>
        /// <param name="actionUrl">Url to retrieve data like: Controller/Action?page</param>
        /// <param name="additionalParameters">Search parameters in the query</param>
        /// <param name="activeFirstLastButton">Display First/Last buttons</param>
        /// <param name="activePreviousNextButton">Display Previous/Next buttons</param>
        public NavBar(int currentPage, int totalPages, string actionUrl, string additionalParameters = "", bool activeFirstLastButton = true, bool activePreviousNextButton = true)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            ActionUrl = actionUrl;

            if (!string.IsNullOrEmpty(additionalParameters))
                QueryParameters = string.Concat("&", additionalParameters);

            _showFirstLastButtons = activeFirstLastButton;
            _showPreviousNextButtons = activePreviousNextButton;
        }

        /// <summary>
        /// Generate HTML to display the nav bar
        /// </summary>
        /// <returns></returns>
        public string DrawButton()
        {
            string navigation = string.Empty;
            var buttons = GetAllButtons(_showFirstLastButtons, _showPreviousNextButtons);

            foreach (var button in buttons)
            {
                navigation = string.Concat(navigation, button.Draw());
            }

            // no html generation
            if (string.IsNullOrEmpty(navigation))
                return string.Empty;

            return string.Format("<nav><ul class='pagination'>{0}</ul></nav>", navigation);
        }

        /// <summary>
        /// Get all generated buttons
        /// </summary>
        /// <param name="activeFirstLastButton">Display First/Last buttons</param>
        /// <param name="activePreviousNextButton">Display Previous/Next buttons</param>
        /// <returns></returns>
        public IEnumerable<Button> GetAllButtons(bool activeFirstLastButton, bool activePreviousNextButton)
        {
            var pageButtons = GetPagesButton();

            // avoid html generation if less or equal than one page
            if (pageButtons.Count <= 1) return new List<Button>();

            // remove first/last and previous/next buttons if necessary
            AutoRemoveButtons(pageButtons.Count, ref activeFirstLastButton, ref activePreviousNextButton);
            
            // insert buttons around of page buttons
            if (activePreviousNextButton)
            {
                pageButtons.Insert(0, new Previous(1, CurrentPage, TotalPages, ActionUrl, QueryParameters));
                pageButtons.Add(new Next(1, CurrentPage, TotalPages, ActionUrl, QueryParameters));
            }

            if (activeFirstLastButton)
            {
                pageButtons.Insert(0, new First(1, CurrentPage, TotalPages, ActionUrl, QueryParameters));
                pageButtons.Add(new Last(1, CurrentPage, TotalPages, ActionUrl, QueryParameters));
            }

            return pageButtons;
        }

        private void AutoRemoveButtons(int numberOfPageButtons, ref bool activeFirstLastButton, ref bool activePreviousNextButton)        
        {
            if (numberOfPageButtons > 1 && numberOfPageButtons <= PAGES_AROUND_CURRENT)
            {
                activeFirstLastButton = false;
                activePreviousNextButton = false;
            }
            else if(numberOfPageButtons > PAGES_AROUND_CURRENT && numberOfPageButtons < MAX_PAGE_TO_DISPLAY)
            {
                activeFirstLastButton = false;
            }
        }
        
        private List<Button> GetPagesButton()
        {
            // the main logic is here
            int pageDisplayed = 0;
            List<Button> pageButton = null;

            // case 1 : the first pages
            if (CurrentPage + PAGES_AROUND_CURRENT <= MAX_PAGE_TO_DISPLAY || MAX_PAGE_TO_DISPLAY > TotalPages)
            {
                pageButton = CreatePageButtons(1, CurrentPage, TotalPages, ref pageDisplayed, ActionUrl, QueryParameters);
            }
            // case 2 : in the middle
            else if (CurrentPage + PAGES_AROUND_CURRENT > MAX_PAGE_TO_DISPLAY && CurrentPage + PAGES_AROUND_CURRENT <= TotalPages)
            {
                var startPage = CurrentPage - PAGES_AROUND_CURRENT;
                pageButton = CreatePageButtons(startPage, CurrentPage, TotalPages, ref pageDisplayed, ActionUrl, QueryParameters);
            }
            // case 3 : the latest pages
            else if ((CurrentPage >= MAX_PAGE_TO_DISPLAY && CurrentPage + PAGES_AROUND_CURRENT > TotalPages) || CurrentPage == TotalPages)
            {
                var startPage = CurrentPage - MAX_PAGE_TO_DISPLAY + (TotalPages - CurrentPage) + 1;
                pageButton = CreatePageButtons(startPage, CurrentPage, TotalPages, ref pageDisplayed, ActionUrl, QueryParameters);
            }

            return pageButton;
        }

        private List<Button> CreatePageButtons(int startPage, int currentPage, int totalPages, ref int pageDisplayed, string actionUrl, string additionalParameters)
        {
            List<Button> pageButtons = new List<Button>();

            for (var page = startPage; page <= totalPages; page++)
            {
                pageButtons.Add(new Page(page, currentPage, totalPages, actionUrl, additionalParameters));

                pageDisplayed++;
                if (MAX_PAGE_TO_DISPLAY == pageDisplayed) break;
            }

            return pageButtons;
        }
    }
}