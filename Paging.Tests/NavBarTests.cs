using System.Linq;
using NUnit.Framework;
using Paging.Buttons;

namespace Paging.Tests
{
    [TestFixture]
    public class NavBarTests
    {
        /// <summary>
        /// For 20 pages all buttons (first/last and previous/next) are displayed
        /// </summary>
        [Test]
        public void AllButtonsAreDisplayed_Test()
        {
            const int currentPage = 7;
            var navbar = new NavBar(currentPage, 20, "Controller/Action");
            var buttons = navbar.GetAllButtons(true, true);

            // 15 = MAX_PAGE_TO_DISPLAY (11) + First + Last + Previous + Next
            Assert.IsTrue(buttons.Count() == 15);

            var firstButton = buttons.First();
            Assert.IsInstanceOf<First>(firstButton);

            var lastButton = buttons.Last();
            Assert.IsInstanceOf<Last>(lastButton);

            var previousButton = buttons.ElementAt(1);
            Assert.IsInstanceOf<Previous>(previousButton);

            var nextButton = buttons.ElementAt(13);
            Assert.IsInstanceOf<Next>(nextButton);

            var pageButton = buttons.OfType<Page>().Count();
            Assert.AreEqual(11, pageButton);

            var selectedPage = buttons.OfType<Page>().Single(btn => btn.IsSelected);
            Assert.AreEqual(currentPage, selectedPage.PageNumber);

            var html = navbar.DrawButton();
            Assert.IsTrue(html.StartsWith("<nav><ul class='pagination'>"));
            Assert.IsTrue(html.EndsWith("</ul></nav>"));
        }


        /// <summary>
        /// For 10 pages only previous and next button are displayed
        /// </summary>
        [Test]
        public void OnlyPreviousAndNextButtons_Test()
        {
            const int currentPage = 3;
            var navbar = new NavBar(currentPage, 10, "Controller/Action");
            var buttons = navbar.GetAllButtons(true, true);

            Assert.IsTrue(buttons.Count() == 12);

            var previousButton = buttons.First();
            Assert.IsInstanceOf<Previous>(previousButton);

            var nextButton = buttons.Last();
            Assert.IsInstanceOf<Next>(nextButton);

            var pageButton = buttons.OfType<Page>().Count();
            Assert.AreEqual(10, pageButton);

            var selectedPage = buttons.OfType<Page>().Single(btn => btn.IsSelected);
            Assert.AreEqual(currentPage, selectedPage.PageNumber);
        }


        /// <summary>
        /// For 5 pages of less no additional button are displayed
        /// </summary>
        [Test]
        public void NoAdditionalButton_Test()
        {
            const int currentPage = 2;
            var navbar = new NavBar(currentPage, 4, "Controller/Action");
            var buttons = navbar.GetAllButtons(true, true);

            Assert.IsTrue(buttons.Count() == 4);

            var pageButton = buttons.OfType<Page>().Count();
            Assert.AreEqual(4, pageButton);

            var selectedPage = buttons.OfType<Page>().Single(btn => btn.IsSelected);
            Assert.AreEqual(currentPage, selectedPage.PageNumber);
        }


        /// <summary>
        /// For 1 page or less no button is displayed
        /// </summary>
        [Test]
        public void NoButtonToDisplay_Test()
        {
            const int currentPage = 1;
            var navbar = new NavBar(currentPage, 1, "Controller/Action");
            var buttons = navbar.GetAllButtons(true, true);

            Assert.IsTrue(!buttons.Any());
            Assert.IsTrue(string.IsNullOrEmpty(navbar.DrawButton()));
        }


        /// <summary>
        /// Ensure first and previous buttons are disabled when the first page is selected (current page)
        /// </summary>
        [Test]
        public void FirstAndPreviousButtonsAreDisabled_Test()
        {
            const int currentPage = 1;
            var navbar = new NavBar(currentPage, 20, "Controller/Action");
            var buttons = navbar.GetAllButtons(true, true);

            Assert.IsTrue(buttons.Count() == 15);

            var firstButton = buttons.First();
            Assert.IsInstanceOf<First>(firstButton);
            Assert.IsTrue(firstButton.IsSelected);

            var lastButton = buttons.Last();
            Assert.IsInstanceOf<Last>(lastButton);
            Assert.IsFalse(lastButton.IsSelected);

            var previousButton = buttons.ElementAt(1);
            Assert.IsInstanceOf<Previous>(previousButton);
            Assert.IsTrue(previousButton.IsSelected);

            var nextButton = buttons.ElementAt(13);
            Assert.IsInstanceOf<Next>(nextButton);
            Assert.IsFalse(nextButton.IsSelected);

            var pageButton = buttons.OfType<Page>().Count();
            Assert.AreEqual(11, pageButton);

            var selectedPage = buttons.OfType<Page>().Single(btn => btn.IsSelected);
            Assert.AreEqual(currentPage, selectedPage.PageNumber);
        }


        /// <summary>
        /// Ensure last and next buttons are disabled when the last page is selected (current page)
        /// </summary>
        [Test]
        public void LastAndNextButtonsAreDisabled_Test()
        {
            const int currentPage = 20;
            var navbar = new NavBar(currentPage, 20, "Controller/Action");
            var buttons = navbar.GetAllButtons(true, true);

            Assert.IsTrue(buttons.Count() == 15);

            var firstButton = buttons.First();
            Assert.IsInstanceOf<First>(firstButton);
            Assert.IsFalse(firstButton.IsSelected);

            var lastButton = buttons.Last();
            Assert.IsInstanceOf<Last>(lastButton);
            Assert.IsTrue(lastButton.IsSelected);

            var previousButton = buttons.ElementAt(1);
            Assert.IsInstanceOf<Previous>(previousButton);
            Assert.IsFalse(previousButton.IsSelected);

            var nextButton = buttons.ElementAt(13);
            Assert.IsInstanceOf<Next>(nextButton);
            Assert.IsTrue(nextButton.IsSelected);

            var pageButton = buttons.OfType<Page>().Count();
            Assert.AreEqual(11, pageButton);

            var selectedPage = buttons.OfType<Page>().Single(btn => btn.IsSelected);
            Assert.AreEqual(currentPage, selectedPage.PageNumber);
        }
    }
}
