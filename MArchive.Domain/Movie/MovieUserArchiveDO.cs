using MArchive.Domain.Base;

namespace MArchive.Domain.Movie {
	public class MovieUserArchiveDO : MovieInfoDO {
		public int ArchiveID { get; set; }

		public string Resolution { get; set; }

		public string FileExtension { get; set; }

		public string Path { get; set; }

		public string ArchiveName { get; set; }
		public string ArchivePath { get; set; }

        public int UserID { get; set; }
	}
}