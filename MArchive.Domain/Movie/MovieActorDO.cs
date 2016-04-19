using MArchive.Domain.Base;

namespace MArchive.Domain.Movie {
	public class MovieActorDO : MovieInfoDO {
		public int ActorID { get; set; }

		public string ActorName { get; set; }
	}
}