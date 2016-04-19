using MArchive.Domain.Base;

namespace MArchive.Domain.Movie
{
    public class UserListMovieDO : BaseDO
    {
        public int ListID { get; set; }
        public int MovieID { get; set; }
        public int SortOrder { get; set; }
    }
}