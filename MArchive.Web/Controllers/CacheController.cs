using System.Web.Mvc;
using com.cagdaskorkut.utility.CacheUtil;
using MArchive.Web.Mvc.BaseControllers;
using com.cagdaskorkut.mvc;

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