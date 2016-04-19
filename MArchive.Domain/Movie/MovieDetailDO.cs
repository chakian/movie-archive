using System.Collections.Generic;
using System.Web.Mvc;

namespace MArchive.Domain.Movie {
	public class MovieDetailDO {
		public int ID {
			get {
				return Movie == null ? 0 : Movie.ID;
			}
		}

		public MovieDO Movie { get; set; }

		public List<MovieNameDO> Names { get; set; }

		public List<MovieUserArchiveDO> Archives { get; set; }

		public List<MovieLanguageDO> Languages { get; set; }

		public List<MovieTypeDO> Types { get; set; }

		public List<MovieDirectorDO> Directors { get; set; }

		public List<MovieWriterDO> Writers { get; set; }
		
		public List<MovieActorDO> Actors { get; set; }

		public List<MovieUserRatingDO> UserRatings { get; set; }

		public double AverageUserRating { get; set; }

		public MovieUserRatingDO CurrentUserRating { get; set; }

        public List<UserListDO> UserLists { get; set; }
        public List<UserListDetailDO> UserListsIncludingThisMovie { get; set; }

        //Non-DB properties
        public IEnumerable<SelectListItem> SelectableLists
        {
            get
            {
                List<SelectListItem> listItems = new List<SelectListItem>();
                listItems.Add(new SelectListItem() { Text = "Select a list", Value = "-1", Selected = true });
                foreach (var item in UserLists)
                {
                    listItems.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.ID.ToString(),
                        Selected = false
                    });
                }
                return listItems;
            }
        }
	}
}