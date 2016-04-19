using Elmah;
using MArchiveLibrary.Exceptions;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace MArchiveLibrary.Attributes
{
    public class HandleErrorCustomAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            Exception e = filterContext.Exception;
            ErrorSignal.FromCurrentContext().Raise(e);

            if ((e is BusinessException) == false)
            {
                e = new Exception("An unexpected exception occured.", e);
            }

            filterContext.Controller.TempData["exception"] = e;
            filterContext.Controller.TempData["controllerName"] = filterContext.RouteData.Values["Controller"];
            filterContext.Controller.TempData["actionName"] = filterContext.RouteData.Values["Action"];

            if (!filterContext.HttpContext.Response.IsRequestBeingRedirected)
            {
                //if( filterContext.HttpContext.Request.IsAjaxRequest( ) ) {
                //	filterContext.Result = new RedirectToRouteResult( new RouteValueDictionary { { "controller", "Error" }, { "action", "InternalPartial" }, { "area", "" } } );
                //} else {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Error" }, { "action", "Internal" }, { "area", "" } });
                //}
            }

            filterContext.ExceptionHandled = true;

            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.StatusDescription = filterContext.Exception.Message;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            filterContext.HttpContext.Response.Clear();
        }
    }
}
