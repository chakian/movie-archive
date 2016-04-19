using SystemType = System.Type;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Reflection;
using System.Web;
using System.Text;
using System.Web.Routing;
using Elmah;
using MArchiveLibrary.Exceptions;

namespace MArchiveLibrary.Attributes
{
    public abstract class AuthorizeUserAttribute : AuthorizeAttribute
    {
        private static bool Authorize(SimpleUserModel user, RoleEnum[] Tags)
        {
            if (Tags == null || Tags.Length == 0)
                return false;

            if (Tags.Contains(RoleEnum.SKIP_AUTHORIZATION))
            {
                if (Tags.Length > 1)
                    throw new SystemException(string.Format("Action cannot contain more then one AuthorizationTag if it already has SKIP attribute"));
                else {
                    return true;
                }
            }

            if (Tags.Contains(RoleEnum.SuperDuperUser))
            {
                return user.IsAdmin;
            }

            //if the user is admin, then he is welcome anyway
            if (user.IsAdmin)
                return true;

            if (Tags.Contains(RoleEnum.LoggedInUser))
            {
                return (user != null && user.ID > 0);
            }

            return false;
        }

        protected SimpleUserModel ReadUserFromSession()
        {
            SimpleUserModel currentUser = (SimpleUserModel)HttpContext.Current.Session["user"];
            return currentUser;
        }
        protected void WriteUserToSession(AuthorizationContext filterContext, SimpleUserModel user)
        {
            filterContext.HttpContext.Session["user"] = user;
        }

        public static bool IsAuthorizedForAction(string actionName, string controllerName)
        {
            bool isAuthorized = false;

            SystemType controller = SystemType.GetType(string.Format(ControllerIdentifier, controllerName));

            if (controller != null)
            {
                MethodInfo[] mArr = controller.GetMethods().Where(mi => mi.Name == actionName).ToArray<MethodInfo>();
                foreach (MethodInfo m in mArr)
                {
                    RoleAttribute[] authTagList = m.GetCustomAttributes(typeof(RoleAttribute), true) as RoleAttribute[];
                    if (authTagList.Length < 1)
                    {
                        authTagList = controller.GetCustomAttributes(typeof(RoleAttribute), true) as RoleAttribute[];
                    }
                    RoleEnum[] authTagArray = authTagList.Select(t => t.Role).ToArray<RoleEnum>();

                    SimpleUserModel currentUser = (SimpleUserModel)HttpContext.Current.Session["user"];
                    if (currentUser == null)
                        currentUser = new SimpleUserModel();

                    isAuthorized = Authorize(currentUser, authTagArray);

                    if (isAuthorized)
                        break;
                }
            }

            return isAuthorized;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (IsAuthorizedForAction(filterContext.ActionDescriptor.ActionName, filterContext.ActionDescriptor.ControllerDescriptor.ControllerName) == false)
                base.HandleUnauthorizedRequest(filterContext);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            InitializeSessionUserIfNecessary(filterContext);

            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;

            bool isAuthorized = false;
            isAuthorized = IsAuthorizedForAction(actionName, controllerName);

            SimpleUserModel currentUser = ReadUserFromSession();
            if (currentUser == null)
                currentUser = new SimpleUserModel();

            if (!isAuthorized)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("User ")
                    .Append(currentUser.Name)
                    .Append(" against: '")
                    .Append(controllerName)
                    .Append("/")
                    .Append(actionName);

                ErrorSignal.FromCurrentContext().Raise(new AuthorizationException(sb.ToString()));

                if (filterContext.IsChildAction == true)
                {
                    // return empty content for child actions..
                    filterContext.Result = new ContentResult();
                }
                else {
                    // redirect page for main actions..
                    if (!filterContext.HttpContext.Response.IsRequestBeingRedirected)
                    {
                        //if( filterContext.HttpContext.Request.IsAjaxRequest( ) ) {
                        //	// current action'un execution'unu kesmek için filterContext'in result'ına redirect assign ediliyor.
                        //	filterContext.Result = new RedirectToRouteResult(
                        //			new RouteValueDictionary { { "controller", "Error" }, { "action", "AccessDeniedPartial" }, { "area", "" } }
                        //			);

                        //	// Eğer bu şekilde yapılırsa kullanıcı redirect olur ama bu action da execute eder. UnAuthorized işlem yapılmış olur.
                        //	// filterContext.HttpContext.Response.Redirect("/Error/AccessDeniedPartial", true);
                        //} else {
                        //	// current action'un execution'unu kesmek için filterContext'in result'ına redirect assign ediliyor.
                        //	filterContext.Result = new RedirectToRouteResult(
                        //		new RouteValueDictionary { { "controller", "Error" }, { "action", "AccessDenied" }, { "area", "" } }
                        //		);
                        //}
                        // current action'un execution'unu kesmek için filterContext'in result'ına redirect assign ediliyor.
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary { { "controller", "Account" }, { "action", "Login" }, { "area", "" } }
                            );
                    }
                }
            }
        }

        /*
        //This property contains the controller path within the project. It looks something like this:
        "MArchive.Web.Controllers.{0}Controller, MArchive.Web"
        This property has to be initialized within the child class
        */
        protected static string ControllerIdentifier = string.Empty;

        /*
        //This function usually looks something like below
        protected override void InitializeSessionUserIfNecessary(AuthorizationContext filterContext)
        {
            IPrincipal contextUser = filterContext.HttpContext.User;
            if (contextUser.Identity.IsAuthenticated)
            {
                if (filterContext.HttpContext.Session["user"] == null)
                {
                    SimpleUserModel user = ConvertFromUserDO(UserBL.GetUserDOByUsername(contextUser.Identity.Name));
                    if (user == null)
                    {
                        FormsAuthentication.SignOut();
                    }
                    WriteUserToSession(filterContext, user);
                }
            }
        }
        */
        protected abstract void InitializeSessionUserIfNecessary(AuthorizationContext filterContext);
    }
}
