﻿using System.Web.Mvc;
using MArchive.Web.Mvc.BaseControllers;
using MArchiveLibrary.Attributes;

namespace MArchive.Web.Controllers {
    [Role(RoleEnum.SKIP_AUTHORIZATION)]
	public class HomeController : MArchiveBaseController
    {
		public ActionResult Index( ) {
			return View( );
		}

        public ActionResult About()
        {
            return View();
        }
	}
}