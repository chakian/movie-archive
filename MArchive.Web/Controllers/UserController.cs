using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MArchive.BL;
using MArchive.Web.Models.User;
using MArchive.Web.Mvc.BaseControllers;

namespace MArchive.Web.Controllers
{
    public class UserController : MArchiveBaseAuthenticatedController
    {
        public ActionResult UserProfile(string username)
        {
            ProfileModel model = new ProfileModel();
            model.User = UserBL.GetUserDOByUsername(username);
            model.IsYou = (model.User.ID == UserID);

            model.FriendStatus = FriendBL.GetFriendshipStatus(UserID, model.User.ID);
            
            return View(model);
        }
    }
}
