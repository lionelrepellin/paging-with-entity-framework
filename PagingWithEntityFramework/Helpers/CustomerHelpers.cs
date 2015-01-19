using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PagingWithEntityFramework.Helpers
{
    public static class CustomHelpers
    {
        public static MvcHtmlString DrawNavBar(this HtmlHelper helper, int currentPage, int totalPages)
        {
            var navBar = new NavBar(currentPage, totalPages, "/Home/Get?page");
            return new MvcHtmlString(navBar.Draw());
        }
    }
}