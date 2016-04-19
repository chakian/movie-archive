using System.ComponentModel.DataAnnotations;

namespace MArchive.Domain.Base {
	public class LookupBaseDO : BaseDO {
		[StringLength ( 100 )]
		public string Name { get; set; }
	}
}