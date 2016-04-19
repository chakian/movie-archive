namespace MArchive.Domain.Movie
{
    public class UserListDetailDO
    {
        public int ListID { get; set; }
        public string ListName { get; set; }
        public int ListMovieID { get; set; }
        public int MovieID { get; set; }
        public string MovieName { get; set; }
        public int SortOrder { get; set; }
        public bool IsChecked { get; set; }
        public bool Watched { get; set; }
    }
}