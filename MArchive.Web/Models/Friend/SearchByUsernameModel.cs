using System.Collections.Generic;
using MArchive.Domain.User;

namespace MArchive.Web.Models.Friend {
    public class SearchByUsernameModel {
        public string SearchKeyword { get; set; }

        public List<UserDO> UserList { get; set; }
    }
}