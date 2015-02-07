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
            var model = CreateModel(new ErrorModel { CurrentPage = 1 });
            return View("Index", model);
        }


        /// <summary>
        /// Used to navigate from page to page (with or without search criteria)
        /// </summary>
        /// <param name="errorModel"></param>
        /// <returns></returns>
        public ActionResult Get(ErrorModel errorModel)
        {
            var model = CreateModel(errorModel);
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
            errorModel.CurrentPage = 1;

            var model = CreateModel(errorModel);
            return View("Index", model);
        }


        // the 'virtual' keyword is used to mock the method
        public virtual ErrorModel CreateModel(ErrorModel errorModel)
        {
            // get an object if search criteria have been defined
            var searchCriteria = errorModel.GetDefinedSearchCriteria();

            // retrieve errors from database
            var result = _errorService.RetrieveErrors(errorModel.CurrentPage, LINES_PER_PAGE, searchCriteria);

            // set properties to the model
            errorModel.Errors = result.Errors;
            errorModel.LinesPerPage = LINES_PER_PAGE;
            errorModel.TotalLines = result.TotalLines;

            return errorModel;
        }
    }
}