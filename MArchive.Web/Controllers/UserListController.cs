using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MArchive.Web.Mvc.BaseControllers;

namespace MArchive.Web.Controllers
{
    public class UserListController : MArchiveBaseAuthenticatedController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}