using System.Linq;
using System.Web.Mvc;
using MArchive.BL;
using MArchive.Domain.Movie;
using MArchive.Web.Models.Search;
using MArchive.Web.Mvc.BaseControllers;
using com.cagdaskorkut.mvc;
using com.cagdaskorkut.mvc.JqGrid.Model;

namespace MArchive.Web.Controllers {
    [Role(RoleEnum.SKIP_AUTHORIZATION)]
	public class SearchController : MArchiveBaseController
    {
		private JqGridData GetGridData( JqGridRequest request, IQueryable<MovieListDO> movieList ) {
			//JqGridRequest request = new JqGridRequest( ) {
			//	_search = false,
			//	filters = null,
			//	page = 1,
			//	rows = 5,
			//	searchField = null,
			//	searchOper = null,
			//	searchString = null,
			//	sidx = "ID",
			//	sord = "asc"
			//};
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

		public ActionResult Super( string keyword ) {
			SetSearchKeyword( keyword );
			if( string.IsNullOrEmpty( keyword ) ) {
				SetErrorMessage( "One doesn't simply do a search without keyword!!!1" );
				return View( );
			}
			SuperSearchResultModel model = new SuperSearchResultModel( );
			model.keyword = keyword;
			//model.resultByName = Json( GetGridData( MovieListBL.SearchSuperbly( keyword ).AsQueryable( ) ) );
			return View( model );
		}

		[HttpPost]
		public ActionResult GetSuperSearchForName( JqGridRequest request ) {
			string keyword = GetSearchKeyword( );
			var movieList = MovieListBL.SearchSuperblyForNames( keyword ).AsQueryable( );
			JqGridData result = GetGridData( request, movieList );
			return Json( result, JsonRequestBehavior.AllowGet );
		}

		[HttpPost]
		public ActionResult GetSuperSearchForActor( JqGridRequest request ) {
			string keyword = GetSearchKeyword( );
			var movieList = MovieListBL.SearchSuperblyForActors( keyword ).AsQueryable( );
			JqGridData result = GetGridData( request, movieList );
			return Json( result, JsonRequestBehavior.AllowGet );
		}

		[HttpPost]
		public ActionResult GetSuperSearchForDirector( JqGridRequest request ) {
			string keyword = GetSearchKeyword( );
			var movieList = MovieListBL.SearchSuperblyForDirectors( keyword ).AsQueryable( );
			JqGridData result = GetGridData( request, movieList );
			return Json( result, JsonRequestBehavior.AllowGet );
		}

		[HttpPost]
		public ActionResult GetSuperSearchForWriter( JqGridRequest request ) {
			string keyword = GetSearchKeyword( );
			var movieList = MovieListBL.SearchSuperblyForWriters( keyword ).AsQueryable( );
			JqGridData result = GetGridData( request, movieList );
			return Json( result, JsonRequestBehavior.AllowGet );
		}

		[HttpPost]
		public ActionResult GetSuperSearchForType( JqGridRequest request ) {
			string keyword = GetSearchKeyword( );
			var movieList = MovieListBL.SearchSuperblyForTypes( keyword ).AsQueryable( );
			JqGridData result = GetGridData( request, movieList );
			return Json( result, JsonRequestBehavior.AllowGet );
		}

		[HttpPost]
		public ActionResult GetSuperSearchForLanguage( JqGridRequest request ) {
			string keyword = GetSearchKeyword( );
			var movieList = MovieListBL.SearchSuperblyForLanguages( keyword ).AsQueryable( );
			JqGridData result = GetGridData( request, movieList );
			return Json( result, JsonRequestBehavior.AllowGet );
		}
	}
}