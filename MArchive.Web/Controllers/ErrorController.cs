using System;
using System.Web.Mvc;
using com.cagdaskorkut.mvc;
using MArchive.Web.Mvc.BaseControllers;

namespace MArchive.Web.Controllers
{
    [Role(RoleEnum.SKIP_AUTHORIZATION)]
    public class ErrorController : MArchiveBaseController
    {
		[HandleError]
		public ActionResult Internal( ) {
			Response.TrySkipIisCustomErrors = true;
			Exception e = ( Exception )TempData["exception"];
			if( e == null )
				e = new Exception( "An unexpected error occured" );
			string contr = ( string )TempData["controllerName"];
			if( string.IsNullOrEmpty( contr ) )
				contr = "Unknown Controller";

			string act = ( string )TempData["actionName"];
			if( string.IsNullOrEmpty( act ) )
				act = "Unknown Action";

			HandleErrorInfo model = new HandleErrorInfo( e, contr, act );
			HttpContext.Response.StatusCode = 500;
			return View( model );
		}
    }
}