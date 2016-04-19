using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;
using MArchive.BL;
using MArchive.Domain.User;
using MArchiveLibrary.Attributes;
using MArchiveLibrary;

namespace MArchive.Web.Mvc.Attributes {
	public class MArchiveAuthorizeUserAttribute : AuthorizeUserAttribute {
        public MArchiveAuthorizeUserAttribute()
        {
            ControllerIdentifier = "MArchive.Web.Controllers.{0}Controller, MArchive.Web";
        }
        
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

        private SimpleUserModel ConvertFromUserDO(UserDO user)
        {
            return new SimpleUserModel()
            {
                ID = user.ID,
                Name = user.Name,
                IsAdmin = user.IsAdmin
            };
        }
	}
}