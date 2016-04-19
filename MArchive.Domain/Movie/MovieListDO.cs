using System.Collections.Generic;

namespace MArchive.Domain.Movie {
	public class MovieListDO {
		public int ID {
			get {
				return Movie == null ? 0 : Movie.ID;
			}
		}

		public MovieDO Movie { get; set; }

		public string OriginalName { get; set; }
		public string EnglishName { get; set; }
		public string TurkishName { get; set; }
	}
}