using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PagingWithEntityFramework.Helpers
{
    public abstract class AbstractNavBar
    {
        protected int CurrentPage;
        protected int TotalPages;
        protected string ActionUrl;

        public AbstractNavBar(int currentPage, int totalPages, string actionUrl)
        {
            this.CurrentPage = currentPage;
            this.TotalPages = totalPages;
            this.ActionUrl = actionUrl;
        }

        public abstract string Draw();
    }

    public class NavBar : AbstractNavBar
    {
        #region FIRST & LAST BUTTONS

        private class FirstLastButton : AbstractNavBar
        {
            public FirstLastButton(int currentPage, int totalPages, string actionUrl)
                : base(currentPage, totalPages, actionUrl)
            {
            }

            public override string Draw()
            {
                var firstButton = CreateFirstButton(CurrentPage, ActionUrl);
                var lastButton = CreateLastButton(CurrentPage, TotalPages, ActionUrl);
                return string.Concat(firstButton, "{0}", lastButton);
            }

            private string CreateFirstButton(int currentPage, string actionUrl)
            {
                string navbar = string.Empty;

                if (currentPage == 1)
                {
                    navbar = "<li class='disabled'><a href='#' aria-label='First page'>";
                }
                else
                {
                    navbar = string.Format("<li><a href='{0}=1' aria-label='First page'>", actionUrl);
                }
                return string.Concat(navbar, "<span class='glyphicon glyphicon-step-backward' aria-hidden='true'></span></a></li>");
            }

            private string CreateLastButton(int currentPage, int totalPages, string actionUrl)
            {
                string navbar = string.Empty;

                if (currentPage == totalPages)
                {
                    navbar = "<li class='disabled'><a href='#' aria-label='Last page'>";
                }
                else
                {
                    navbar = string.Format("<li><a href='{0}={1}' aria-label='Last page'>", actionUrl, totalPages);
                }
                return string.Concat(navbar, "<span class='glyphicon glyphicon-step-forward' aria-hidden='true'></span></a></li>");
            }
        }

        #endregion FIRST & LAST BUTTONS

        #region PREVIOUS & NEXT BUTTONS

        private class PrevNextButton : AbstractNavBar
        {
            public PrevNextButton(int currentPage, int totalPages, string actionUrl)
                : base(currentPage, totalPages, actionUrl)
            {
            }

            public override string Draw()
            {
                var previousButton = CreatePreviousButton(CurrentPage, ActionUrl);
                var nextButton = CreateNextButton(CurrentPage, TotalPages, ActionUrl);
                return string.Concat(previousButton, "{0}", nextButton);
            }

            private string CreatePreviousButton(int currentPage, string actionUrl)
            {
                string navbar = string.Empty;

                if (currentPage == 1)
                {
                    navbar = "<li class='disabled'><a href='#' aria-label='Previous'>";
                }
                else
                {
                    navbar = string.Format("<li><a href='{0}={1}' aria-label='Previous'>", actionUrl, currentPage - 1);
                }
                return string.Concat(navbar, "<span class='glyphicon glyphicon-chevron-left' aria-hidden='true'></span></a></li>");
            }

            private string CreateNextButton(int currentPage, int totalPages, string actionUrl)
            {
                string navbar = string.Empty;

                if (currentPage == totalPages)
                {
                    navbar = "<li class='disabled'><a href='#' aria-label='Next'>";
                }
                else
                {
                    navbar = string.Format("<li><a href='{0}={1}' aria-label='Next'>", actionUrl, currentPage + 1);
                }
                return string.Concat(navbar, "<span class='glyphicon glyphicon-chevron-right' aria-hidden='true'></span></a></li>");
            }
        }

        #endregion PREVIOUS & NEXT BUTTONS

        private const int MAX_PAGE_TO_DISPLAY = 11;
        private const int PAGES_AROUND_CURRENT = 5;

        private List<AbstractNavBar> additionalButtons;
        private bool showFirstLastButtons;
        private bool showPreviousNextButtons;

        public NavBar(int currentPage, int totalPages, string actionUrl, bool activeFirstLastButton = true, bool activePreviousNextButton = true)
            : base(currentPage, totalPages, actionUrl)
        {
            this.additionalButtons = new List<AbstractNavBar>();
            this.showFirstLastButtons = activeFirstLastButton;
            this.showPreviousNextButtons = activePreviousNextButton;
        }

        public override string Draw()
        {
            string navigation = string.Empty;
            string additionalButtons = GetAdditionalNavButtons();

            if (string.IsNullOrEmpty(additionalButtons))
            {
                navigation = GetPages();
            }
            else
            {
                navigation = string.Format(additionalButtons, GetPages());
            }
            return string.Format("<nav><ul class='pagination'>{0}</ul></nav>", navigation);
        }

        private string GetPages()
        {
            // the main logic is here
            int pageDisplayed = 0;
            string pages = string.Empty;

            // case 1 : the first pages
            if (CurrentPage + PAGES_AROUND_CURRENT <= MAX_PAGE_TO_DISPLAY || MAX_PAGE_TO_DISPLAY > TotalPages)
            {
                pages = CreatePageButtons(1, CurrentPage, TotalPages, ref pageDisplayed, ActionUrl);
            }
            // case 2 : in the middle
            else if (CurrentPage + PAGES_AROUND_CURRENT > MAX_PAGE_TO_DISPLAY && CurrentPage + PAGES_AROUND_CURRENT <= TotalPages)
            {
                var startPage = CurrentPage - PAGES_AROUND_CURRENT;
                pages = CreatePageButtons(startPage, CurrentPage, TotalPages, ref pageDisplayed, ActionUrl);
            }
            // case 3 : the latest pages
            else if ((CurrentPage >= MAX_PAGE_TO_DISPLAY && CurrentPage + PAGES_AROUND_CURRENT > TotalPages) || CurrentPage == TotalPages)
            {
                var startPage = CurrentPage - MAX_PAGE_TO_DISPLAY + (TotalPages - CurrentPage) + 1;
                pages = CreatePageButtons(startPage, CurrentPage, TotalPages, ref pageDisplayed, ActionUrl);
            }
            return pages;
        }

        // return a list of pages
        private string CreatePageButtons(int startPage, int currentPage, int totalPages, ref int pageDisplayed, string actionUrl)
        {
            string navbar = string.Empty;

            for (var page = startPage; page <= totalPages; page++)
            {
                if (page == currentPage)
                {
                    navbar += string.Format("<li class='active'><a href='#'>{0}</a></li>", currentPage);
                }
                else
                {
                    navbar += string.Format("<li><a href='{0}={1}'>{1}</a></li>", actionUrl, page);
                }

                pageDisplayed++;
                if (MAX_PAGE_TO_DISPLAY == pageDisplayed) break;
            }
            return navbar;
        }

        private string GetAdditionalNavButtons()
        {
            string navbar = string.Empty;

            if (showPreviousNextButtons) additionalButtons.Add(new PrevNextButton(CurrentPage, TotalPages, ActionUrl));
            if (showFirstLastButtons) additionalButtons.Add(new FirstLastButton(CurrentPage, TotalPages, ActionUrl));

            foreach (var button in additionalButtons)
            {
                if (string.IsNullOrEmpty(navbar))
                {
                    navbar = button.Draw();
                }
                else
                {
                    navbar = string.Format(button.Draw(), navbar);
                }
            }
            return navbar;
        }
    }
}