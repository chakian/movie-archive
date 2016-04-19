using MArchive.Domain.Base;

namespace MArchive.Domain.Movie {
	public class MovieLanguageDO : MovieInfoDO {
		public int LanguageID { get; set; }

		public string LanguageName { get; set; }
	}
}