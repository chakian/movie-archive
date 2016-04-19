using System.Collections.Generic;

namespace MArchive.DataContext
{
    public partial class USR_List
    {
        public List<USR_ListMovie> Movies { get; set; }
        public List<USR_UserListAuthorization> AuthorizedUsers { get; set; }
    }
}