namespace MArchive.Domain.User
{
    public class FriendStatus
    {
        public int ThisUser { get; set; }
        public int ThatUser { get; set; }
        public FriendRequestStatusType RequestStatusType { get; set; }
    }

    public enum FriendRequestStatusType
    {
        None = 0,
        Friends = 2,
        Requested = 1,
        Rejected = -1,
        Blocked = -2,

        //Non-Database Items
        TheyRequested = -9999,
        TheyRejected = -9998,
        TheyBlocked = -9997
    }
}
