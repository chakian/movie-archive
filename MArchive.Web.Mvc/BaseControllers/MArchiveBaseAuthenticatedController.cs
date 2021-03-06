﻿using MArchive.Web.Mvc.Attributes;
using MArchiveLibrary.Attributes;

namespace MArchive.Web.Mvc.BaseControllers {
    [MArchiveAuthorizeUser]
    [Role(RoleEnum.LoggedInUser)]
    public class MArchiveBaseAuthenticatedController : MArchiveBaseController {
        public MArchiveBaseAuthenticatedController() {
            if (CurrentUser.ID <= 0) {
                RedirectToAction("Logout", "Account");
            }
        }
    }
}