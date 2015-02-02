using PagingWithEntityFramework.Business;
using PagingWithEntityFramework.Domain;
using PagingWithEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PagingWithEntityFramework.Controllers
{
    public class HomeController : Controller
    {
        private const int LINES_PER_PAGE = 20;
        private ErrorService _errorService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorService">ErrorService injected by Unity</param>
        public HomeController(ErrorService errorService)
        {
            if (errorService == null)
                throw new ArgumentNullException("errorService");
            
            _errorService = errorService;
        }


        /// <summary>
        /// Used to display the first page without search criteria
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = CreateModel(1);
            return View("Index", model);
        }


        /// <summary>
        /// Used to navigate from page to page (with or without search criteria)
        /// </summary>
        /// <param name="errorModel"></param>
        /// <returns></returns>
        public ActionResult Get(ErrorModel errorModel)
        {
            var model = CreateModel(errorModel.CurrentPage, errorModel.Name, errorModel.ErrorLevel, errorModel.StackTrace);
            return View("Index", model);
        }


        /// <summary>
        /// Used to retrieve errors with search criteria
        /// </summary>
        /// <param name="errorModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Search(ErrorModel errorModel)
        {
            // always set the page to 1 to display the result
            var model = CreateModel(1, errorModel.Name, errorModel.ErrorLevel, errorModel.StackTrace);
            return View("Index", model);
        }


        private ErrorModel CreateModel(int currentPage, string name = "", string errorLevel = "", string stackTrace = "")
        {
            SearchCriteria searchCriteria = null;

            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(errorLevel) || !string.IsNullOrEmpty(stackTrace))
            {
                // fill the search criteria object
                searchCriteria = new SearchCriteria
                {
                    StackTrace = stackTrace,
                    ServerName = name,
                    Severity = errorLevel
                };
            }

            // retrieve errors from database
            var result = _errorService.RetrieveErrors(currentPage, LINES_PER_PAGE, searchCriteria);

            // create model
            var errorModel = new ErrorModel
            {
                Errors = result.Errors,
                LinesPerPage = LINES_PER_PAGE,
                CurrentPage = currentPage,
                TotalLines = result.TotalLines,

                // attaches to the model the data we are looking for
                Name = name,
                StackTrace = stackTrace,
                ErrorLevel = errorLevel
            };

            return errorModel;
        }
    }
}