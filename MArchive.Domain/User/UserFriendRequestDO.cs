using MArchive.Domain.Base;
using System;

namespace MArchive.Domain.User
{
    public class UserFriendRequestDO:BaseDO
    {
        public int RequestCreatorUserID { get; set; }
        public int RequestSentToUserID { get; set; }
        public int RequestStatus { get; set; }
        public DateTime InsertDate { get; set; }
    }
}