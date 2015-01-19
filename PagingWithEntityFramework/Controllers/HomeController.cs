using PagingWithEntityFramework.DAL;
using PagingWithEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PagingWithEntityFramework.Controllers
{
    public class HomeController : Controller
    {
        private readonly int linesPerPage = 30;

        public HomeController()
        {
        }

        // GET: /Home/
        public ActionResult Index()
        {
            using (var ctx = new Context())
            {
                var model = CreateErrorModel(0, ctx);
                return View(model);
            }
        }

        // GET: /Home/Get?page=2
        public ActionResult Get(int page)
        {
            using (var ctx = new Context())
            {
                var model = CreateErrorModel(page - 1, ctx);
                return View("Index", model);
            }
        }

        private ErrorModel CreateErrorModel(int pageIndex, Context ctx)
        {
            var model = new ErrorModel
            (
                errors: ctx.GetErrorsByPageIndex(pageIndex, linesPerPage),
                linesPerPage: linesPerPage,
                currentPage: pageIndex + 1,
                totalLines: ctx.GetTotalNumberOfErrors()
            );
            return model;
        }
    }
}
