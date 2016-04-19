using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MArchive.BL;
using MArchive.Web.Models.Friend;
using MArchive.Web.Mvc.BaseControllers;
using MArchive.Domain.User;

namespace MArchive.Web.Controllers
{
    //[Authorize(Roles=MArchive.Web.Mvc.Attributes.MArchiveRoleEnum.LoggedInUser.ToString())]
    public class FriendController : MArchiveBaseAuthenticatedController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchByUsername() {
            SearchByUsernameModel model = new SearchByUsernameModel();
            model.UserList = new List<Domain.User.UserDO>();

            return View(model);
        }

        [HttpPost]
        public ActionResult SearchByUsername(SearchByUsernameModel model) {
            if (model.SearchKeyword.Length >= 5) {
                model.UserList = UserBL.SearchByLikelyUsername(model.SearchKeyword);
            } else {
                model.UserList = UserBL.SearchByExactUsername(model.SearchKeyword);
            }
            return View(model);
        }

        public ActionResult AddFriend(int userId)
        {
            UserFriendRequestDO requestDO = new UserFriendRequestDO();
            requestDO.RequestCreatorUserID = this.UserID;
            requestDO.RequestSentToUserID = userId;
            requestDO.RequestStatus = (int)FriendRequestStatusType.Requested;
            FriendBL.SendFriendRequest(requestDO);

            string userName = UserBL.GetAllUsersDO().First(q => q.ID == userId).Username;

            return RedirectToAction("UserProfile", "User", new { username = userName });
        }

        public ActionResult AcceptFriendRequest(int userId)
        {
            FriendBL.AcceptFriendRequest(UserID, userId);

            string userName = UserBL.GetAllUsersDO().First(q => q.ID == userId).Username;
            return RedirectToAction("UserProfile", "User", new { username = userName });
        }

        public ActionResult RejectFriendRequest(int userId)
        {
            FriendBL.RejectFriendRequest(UserID, userId);

            string userName = UserBL.GetAllUsersDO().First(q => q.ID == userId).Username;
            return RedirectToAction("UserProfile", "User", new { username = userName });
        }
    }
}