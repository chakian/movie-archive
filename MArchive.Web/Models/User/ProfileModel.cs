using MArchive.Domain.User;

namespace MArchive.Web.Models.User {
    public class ProfileModel {
        public UserDO User { get; set; }
        public FriendStatus FriendStatus { get; set; }

        public bool IsYou { get; set; }
        public bool IsYourFriend { get { return FriendStatus.RequestStatusType == FriendRequestStatusType.Friends; } }
        public bool IsRequestSent { get { return FriendStatus.RequestStatusType == FriendRequestStatusType.Requested; } }
        public bool IsRequestPendingApproval { get { return FriendStatus.RequestStatusType == FriendRequestStatusType.TheyRequested; } }
    }
}
