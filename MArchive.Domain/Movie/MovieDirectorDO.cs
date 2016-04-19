using MArchive.Domain.Base;

namespace MArchive.Domain.Movie {
	public class MovieDirectorDO : MovieInfoDO {
		public int DirectorID { get; set; }

		public string DirectorName { get; set; }
	}
}