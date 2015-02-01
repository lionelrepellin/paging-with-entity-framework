﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paging;
using Paging.Buttons;
using System.Linq;

namespace Paging.Tests
{
    [TestClass]
    public class NavBar_Tests
    {
        /// <summary>
        /// For 20 pages all buttons (first/last and previous/next) are displayed
        /// </summary>
        [TestMethod]
        public void AllButtonsAreDisplayed_Test()
        {
            var currentPage = 5;
            var navbar = new NavBar(currentPage, 20, "Controller/Action");
            var buttons = navbar.GetAllButtons(true, true);

            // 15 = MAX_PAGE_TO_DISPLAY (11) + First + Last + Previous + Next
            Assert.IsTrue(buttons.Count() == 15);

            var firstButton = buttons.First();
            Assert.IsInstanceOfType(firstButton, typeof(First));

            var lastButton = buttons.Last();
            Assert.IsInstanceOfType(lastButton, typeof(Last));

            var previousButton = buttons.ElementAt(1);
            Assert.IsInstanceOfType(previousButton, typeof(Previous));

            var nextButton = buttons.ElementAt(13);
            Assert.IsInstanceOfType(nextButton, typeof(Next));

            var pageButton = buttons.OfType<Page>().Count();
            Assert.AreEqual(11, pageButton);

            var selectedPage = buttons.OfType<Page>().Where(btn => btn.IsSelected).Single();
            Assert.AreEqual(currentPage, selectedPage.PageNumber);
        }


        /// <summary>
        /// For 10 pages only previous and next button are displayed
        /// </summary>
        [TestMethod]
        public void OnlyPreviousAndNextButtons_Test()
        {
            var currentPage = 3;
            var navbar = new NavBar(currentPage, 10, "Controller/Action");
            var buttons = navbar.GetAllButtons(true, true);

            Assert.IsTrue(buttons.Count() == 12);

            var previousButton = buttons.First();
            Assert.IsInstanceOfType(previousButton, typeof(Previous));

            var nextButton = buttons.Last();
            Assert.IsInstanceOfType(nextButton, typeof(Next));

            var pageButton = buttons.OfType<Page>().Count();
            Assert.AreEqual(10, pageButton);

            var selectedPage = buttons.OfType<Page>().Where(btn => btn.IsSelected).Single();
            Assert.AreEqual(currentPage, selectedPage.PageNumber);
        }


        /// <summary>
        /// For 5 pages of less no additional button are displayed
        /// </summary>
        [TestMethod]
        public void NoAdditionalButton_Test()
        {
            var currentPage = 2;
            var navbar = new NavBar(currentPage, 4, "Controller/Action");
            var buttons = navbar.GetAllButtons(true, true);

            Assert.IsTrue(buttons.Count() == 4);

            var pageButton = buttons.OfType<Page>().Count();
            Assert.AreEqual(4, pageButton);

            var selectedPage = buttons.OfType<Page>().Where(btn => btn.IsSelected).Single();
            Assert.AreEqual(currentPage, selectedPage.PageNumber);
        }


        /// <summary>
        /// For 1 page or less no button is displayed
        /// </summary>
        [TestMethod]
        public void NoButtonToDisplay_Test()
        {
            var currentPage = 1;
            var navbar = new NavBar(currentPage, 1, "Controller/Action");
            var buttons = navbar.GetAllButtons(true, true);

            Assert.IsTrue(buttons.Count() == 0);
        }


        /// <summary>
        /// Ensure first and previous buttons are disabled when the first page is selected (current page)
        /// </summary>
        [TestMethod]
        public void FirstAndPreviousButtonsAreDisabled_Test()
        {
            var currentPage = 1;
            var navbar = new NavBar(currentPage, 20, "Controller/Action");
            var buttons = navbar.GetAllButtons(true, true);

            Assert.IsTrue(buttons.Count() == 15);

            var firstButton = buttons.First();
            Assert.IsInstanceOfType(firstButton, typeof(First));
            Assert.IsTrue(firstButton.IsSelected);

            var lastButton = buttons.Last();
            Assert.IsInstanceOfType(lastButton, typeof(Last));
            Assert.IsFalse(lastButton.IsSelected);

            var previousButton = buttons.ElementAt(1);
            Assert.IsInstanceOfType(previousButton, typeof(Previous));
            Assert.IsTrue(previousButton.IsSelected);

            var nextButton = buttons.ElementAt(13);
            Assert.IsInstanceOfType(nextButton, typeof(Next));
            Assert.IsFalse(nextButton.IsSelected);

            var pageButton = buttons.OfType<Page>().Count();
            Assert.AreEqual(11, pageButton);

            var selectedPage = buttons.OfType<Page>().Where(btn => btn.IsSelected).Single();
            Assert.AreEqual(currentPage, selectedPage.PageNumber);
        }


        /// <summary>
        /// Ensure last and next buttons are disabled when the last page is selected (current page)
        /// </summary>
        [TestMethod]
        public void LastAndNextButtonsAreDisabled_Test()
        {
            var currentPage = 20;
            var navbar = new NavBar(currentPage, 20, "Controller/Action");
            var buttons = navbar.GetAllButtons(true, true);

            Assert.IsTrue(buttons.Count() == 15);

            var firstButton = buttons.First();
            Assert.IsInstanceOfType(firstButton, typeof(First));
            Assert.IsFalse(firstButton.IsSelected);

            var lastButton = buttons.Last();
            Assert.IsInstanceOfType(lastButton, typeof(Last));
            Assert.IsTrue(lastButton.IsSelected);

            var previousButton = buttons.ElementAt(1);
            Assert.IsInstanceOfType(previousButton, typeof(Previous));
            Assert.IsFalse(previousButton.IsSelected);

            var nextButton = buttons.ElementAt(13);
            Assert.IsInstanceOfType(nextButton, typeof(Next));
            Assert.IsTrue(nextButton.IsSelected);

            var pageButton = buttons.OfType<Page>().Count();
            Assert.AreEqual(11, pageButton);

            var selectedPage = buttons.OfType<Page>().Where(btn => btn.IsSelected).Single();
            Assert.AreEqual(currentPage, selectedPage.PageNumber);
        }
    }
}
