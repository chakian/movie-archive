using System;
using System.Web.Mvc;
using Elmah;
using MArchive.Domain;

namespace MArchive.Web.Mvc.Attributes {
	public class HandleJsonErrorAttribute : FilterAttribute, IExceptionFilter {
		public void OnException( ExceptionContext filterContext ) {
			if( filterContext.ExceptionHandled )
				return;

			if( filterContext.Exception.GetType( ).Name == "JsonValidationException" ) {
			}

			Exception e = filterContext.Exception;
			filterContext.Controller.TempData["exception"] = e;

			MArchiveJsonResult marchiveJsonResponse = new MArchiveJsonResult( );
			marchiveJsonResponse.isSuccess = false;
			marchiveJsonResponse.data = filterContext.Exception.Message;

			if( ( e is BusinessException ) == false ) {
				marchiveJsonResponse.data = "An unexpected exception occured.";
			}
			ErrorSignal.FromCurrentContext( ).Raise( e );

			JsonResult actionResponse = new JsonResult( );
			actionResponse.Data = marchiveJsonResponse;
			actionResponse.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
			filterContext.Result = actionResponse;

			filterContext.ExceptionHandled = true;

			filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
			filterContext.HttpContext.Response.StatusCode = 500;
			filterContext.HttpContext.Response.StatusDescription = filterContext.Exception.Message;
			filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
			filterContext.HttpContext.Response.Clear( );
		}
	}
}