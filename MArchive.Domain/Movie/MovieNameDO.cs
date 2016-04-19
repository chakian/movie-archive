using MArchive.Domain.Base;

namespace MArchive.Domain.Movie {
	public class MovieNameDO : MovieInfoDO {
		public int LanguageID { get; set; }

		public string Name { get; set; }

		public bool IsDefault { get; set; }

		public string LanguageName { get; set; }
	}
}