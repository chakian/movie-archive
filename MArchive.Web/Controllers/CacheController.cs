using System.Web.Mvc;
using MArchive.Web.Mvc.BaseControllers;
using MArchiveLibrary.Attributes;
using MArchiveLibrary.Caching;

namespace MArchive.Web.Controllers {
	[Role( RoleEnum.SuperDuperUser )]
	public class CacheController : MArchiveBaseAuthenticatedController {
		public ActionResult MemoryStatus( ) {
			CacheDTO model = CacheManager.GetCacheDTO( );
			return View( model );
		}

		public RedirectToRouteResult Invalidate( string cacheName ) {
			CacheManager.InvalidateCache( cacheName );
			return RedirectToAction( "MemoryStatus" );
		}

		public RedirectToRouteResult InvalidateAll( ) {
			CacheManager.InvalidateAll( );
			return RedirectToAction( "MemoryStatus" );
		}
	}
}