using System.Web.Mvc;
using System.Linq;
using MArchive.BL;
using MArchive.Domain.Movie;
using MArchive.Web.Models.List;
using System.Collections.Generic;
using MArchive.Web.Mvc.BaseControllers;
using MArchiveLibrary.Attributes;
using MArchiveLibrary.JqGrid.Model;

namespace MArchive.Web.Controllers {
    [Role(RoleEnum.SKIP_AUTHORIZATION)]
	public class MovieListController : MArchiveBaseController
    {
        #region Main Views
        public ActionResult Index( ) {
            List<UserListDO> model = UserListBL.GetAllForUser(UserID);
            return View(model);
		}

		public ActionResult ListAll( ) {
			return View( );
		}

		public ActionResult ListAllInDiscs( ) {
			return View( );
		}

		public ActionResult ListThisYear( ) {
			return View( );
		}

		public ActionResult ListLastYear( ) {
			return View( );
		}
        #endregion Main Views

        #region User's Lists Section
        public ActionResult CreateNewList()
        {
            ListMainModel model = new ListMainModel();
            model.UserList = new UserListDO();
            model.UserList.UserID = UserID;

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateNewList(ListMainModel model)
        {
            model.UserList.UserID = UserID;

            UserListDO savedObject = UserListBL.SaveUserList(model.UserList);
            if (savedObject.ID > 0)
            {
                TempData["InfoMessage"] = "List created";
                return RedirectToAction("ViewList", new { id = savedObject.ID });
            }
            else
            {
                TempData["ErrorMessage"] = "Error occurred while creating list!";
            }

            return View(model);
        }

        public ActionResult ViewList(int id)
        {
            ListDetailViewModel model = new ListDetailViewModel();

            model.List = UserListBL.GetListObject(id);
            model.Movies = UserListBL.GetAllViewByListID(id);

            return View(model);
        }

        public ActionResult EditList(int id)
        {
            ListDetailViewModel model = new ListDetailViewModel();

            model.List = UserListBL.GetListObject(id);
            model.Movies = UserListBL.GetAllViewByListID(id);

            return View(model);
        }
        #endregion User's Lists Section

        #region Grid Things
        private JqGridData GetGridData( JqGridRequest request, IQueryable<MovieListDO> movieList ) {
			return JqGridHelper<MovieListDO>.ToJqGridData( request, movieList, m => new JqGridRowItem {
				id = m.Movie.ID,
				cell = new object[]
                        {
							m.Movie.ID,
							GetFullPathForImage(m.Movie.ImdbPosterPath),
							m.OriginalName,
							m.EnglishName,
							m.TurkishName,
							m.Movie.Year,
							m.Movie.ImdbRating,
							m.Movie.ImdbID,
							GetFormattedDate(m.Movie.InsertDate),
							GetFormattedDate(m.Movie.UpdateDate)
                        }
			} );
		}

		[HttpPost]
		public ActionResult ListAllJson( JqGridRequest request ) {
			var movieList = MovieListBL.GetAllForListing( ).AsQueryable( );
			JqGridData result = GetGridData( request, movieList );
			return Json( result, JsonRequestBehavior.AllowGet );
		}

		[HttpPost]
		public ActionResult ListAllInDiscsJson( JqGridRequest request ) {
			var movieList = MovieListBL.GetAllInDiscsForListing( ).AsQueryable( );
			JqGridData result = GetGridData( request, movieList );
			return Json( result, JsonRequestBehavior.AllowGet );
		}

		[HttpPost]
		public ActionResult ListThisYearJson( JqGridRequest request ) {
			var movieList = MovieListBL.GetThisYearForListing( ).AsQueryable( );
			JqGridData result = GetGridData( request, movieList );
			return Json( result, JsonRequestBehavior.AllowGet );
		}

		[HttpPost]
		public ActionResult ListLastYearJson( JqGridRequest request ) {
			var movieList = MovieListBL.GetLastYearForListing( ).AsQueryable( );
			JqGridData result = GetGridData( request, movieList );
			return Json( result, JsonRequestBehavior.AllowGet );
        }
        #endregion Grid Things
    }
}