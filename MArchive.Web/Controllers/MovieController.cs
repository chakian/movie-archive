using System.Web.Mvc;
using MArchive.BL;
using MArchive.Domain.Movie;
using MArchive.Web.Mvc.Attributes;
using MArchive.Web.Mvc.BaseControllers;
using com.cagdaskorkut.mvc;

namespace MArchive.Web.Controllers
{
    [Role(RoleEnum.SKIP_AUTHORIZATION)]
    public class MovieController : MArchiveBaseController
    {
		public ActionResult DetailView( int id ) {
			MovieDetailDO movie = MovieBL.GetMovieWithDetails( id, UserID );
			return View( movie );
		}

		[HandleJsonError]
		[HttpPost]
		[Role(RoleEnum.LoggedInUser )]
		public JsonResult UpdateUserVote( int UserRatingID, int UserID, int MovieID, bool Watched, string Rating ) {
			MovieUserRatingDO model = new MovieUserRatingDO( ) {
				ID = UserRatingID,
				UserID = UserID,
				MovieID = MovieID,
				Watched = Watched,
				Rating = int.Parse(Rating)
			};
			if( model.Watched == false ) model.Rating = -1;
			MovieUserRatingBL.Save( model, UserID );
			return Json( "ok" );
		}

        [HandleJsonError]
        [HttpPost]
        [Role(RoleEnum.LoggedInUser)]
        public JsonResult AddToList(int ListID, int UserID, int MovieID)
        {
            UserListBL.SaveToList(ListID, MovieID, UserID);
            return Json("ok");
        }

		//public void Add()
		//{
		//	MovieDO movie = new MovieDO();
		//	movie.Year = 1995;
		//	MovieBL.Save(movie, 1);
		//}

		//public void Edit(int _ID)
		//{
		//	MovieDO movie = MovieBL.GetMovie(_ID);
		//	movie.Year = 1996;
		//	movie.ImdbID = null;
		//	MovieBL.Save(movie, 1);
		//}
    }
}