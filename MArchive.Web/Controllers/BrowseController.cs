using System.Linq;
using System.Web.Mvc;
using MArchive.BL;
using MArchive.Web.Models.Browse;
using MArchive.Web.Filtering;
using com.cagdaskorkut.mvc;
using MArchive.Web.Mvc.BaseControllers;

namespace MArchive.Web.Controllers
{
    [Role(RoleEnum.SKIP_AUTHORIZATION)]
    public class BrowseController : MArchiveBaseController
    {
        public ActionResult Index(int page = 1)
        {
            BrowseViewModel model = new BrowseViewModel();

            model.MovieTypes = TypeBL.GetAllDO().OrderBy(q => q.Name).ToList();
            model.MovieList = MovieBL.GetAllAsDO().OrderBy(q => q.OriginalName).ToList();
            model.CurrentPage = page;

            if (Session["BrowseFilters"] != null)
            {
                model.Filters = (FiltersForPage) Session["BrowseFilters"];
                model.MovieList = model.Filters.DoFilter(model.MovieList, UserID).OrderBy(q => q.OriginalName).ToList();
            }
            else
            {
                model.Filters = new Filtering.FiltersForPage();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(BrowseViewModel model)
        {
            model.CurrentPage = 1;
            model.MovieTypes = TypeBL.GetAllDO().OrderBy(q => q.Name).ToList();

            var movieList = MovieBL.GetAllAsDO();

            //save filter to session
            Session["BrowseFilters"] = model.Filters;

            //do the filtering
            movieList = model.Filters.DoFilter(movieList, UserID);

            model.MovieList = movieList.OrderBy(q => q.OriginalName).ToList();
            return View(model);
        }
    }
}