using System.Web.Mvc;
using Paging;

namespace PagingWithEntityFramework.Helpers
{
    public static class CustomHelpers
    {
        public static MvcHtmlString DrawNavBar(this HtmlHelper helper, int currentPage, int totalPages, string additionalParameters)
        {
            // CurrentPage corresponding to the same property name in the model
            var navBar = new NavBar(currentPage, totalPages, "/Home/Get?CurrentPage", additionalParameters);
            return new MvcHtmlString(navBar.DrawButton());
        }
    }
}