using MArchive.Domain.Base;
using System;

namespace MArchive.Domain.User
{
    public class UserFriendDO : BaseDO
    {
        public int UserID { get; set; }
        public int FriendUserID { get; set; }
        public DateTime InsertDate { get; set; }
    }
}