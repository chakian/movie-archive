using MArchive.Domain.Base;

namespace MArchive.Domain.Movie {
	public class MovieTypeDO : MovieInfoDO {
		public int TypeID { get; set; }

		public string TypeName { get; set; }
	}
}