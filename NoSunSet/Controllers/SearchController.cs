using NoSunSet.Models;
using NoSunSet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NoSunSet.Controllers
{
    public class SearchController : Controller
    {
        const String VIEW = "Index";
        const String NO_DATA_PARTIAL_VIEW = "_NoDataPartial";
        const String RESULT_PARTIAL_VIEW = "_SearchResultsPartial";
        const String NO_RESULT_MESSAGE = "No data found.";
        const String EXCEPTION_MESSAGE = "Sorry could not retrieve data.";

        ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public ActionResult Index()
        {
            return View(VIEW);
        }

        public async Task<ActionResult> Find(String registrationNumber)
        {
            SearchResultModel model = new SearchResultModel();

            IEnumerable<Car> data = await Task.Run(() => searchService.GetCarDisplayInformation(registrationNumber));
            model.SearchResults = data;

            if (model.SearchResults.Count() == 0)
            {
                return PartialView(NO_DATA_PARTIAL_VIEW, NO_RESULT_MESSAGE);
            }
            else
            {
                return PartialView(RESULT_PARTIAL_VIEW, model.SearchResults);
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;

            filterContext.Result = new PartialViewResult()
            {
                ViewName = NO_DATA_PARTIAL_VIEW,
                ViewData = new ViewDataDictionary(EXCEPTION_MESSAGE)
            };
        }
    }
}