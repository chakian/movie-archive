using MArchiveLibrary;
using System;
using System.Web.Mvc;
using System.Web.SessionState;

namespace MArchive.Web.Mvc.BaseControllers
{
    public class BaseController : Controller
    {
        protected HttpSessionState WebSession { get { return System.Web.HttpContext.Current.Session; } }

        public virtual SimpleUserModel CurrentUser
        {
            get
            {
                if (WebSession["user"] != null)
                {
                    return ((SimpleUserModel)WebSession["user"]);
                }
                else {
                    return new SimpleUserModel();
                }
            }
        }

        public virtual int UserID
        {
            get
            {
                if (WebSession["user"] != null)
                    return ((SimpleUserModel)WebSession["user"]).ID;
                else
                    return -1;
            }
        }

        public virtual string NameOfUser
        {
            get
            {
                if (WebSession["user"] != null)
                    return ((SimpleUserModel)WebSession["user"]).Name;
                else
                    return string.Empty;
            }
        }

        public string GetFormattedDate(DateTime date)
        {
            return string.Format("{0:yyyy-MM-dd}", date);
        }
        public string GetFormattedDate(DateTime? date)
        {
            if (date.HasValue)
            {
                return string.Format("{0:yyyy-MM-dd}", date.Value);
            }
            else {
                return string.Empty;
            }
        }

        protected void SetInfoMessage(string message)
        {
            TempData["InfoMessage"] = message;
        }

        protected void SetErrorMessage(string message)
        {
            TempData["ErrorMessage"] = message;
        }
    }
}
