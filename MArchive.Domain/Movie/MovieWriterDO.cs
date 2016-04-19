using MArchive.Domain.Base;

namespace MArchive.Domain.Movie {
	public class MovieWriterDO : MovieInfoDO {
		public int WriterID { get; set; }

		public string WriterName { get; set; }
	}
}