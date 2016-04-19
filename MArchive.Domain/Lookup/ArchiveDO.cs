using System.ComponentModel.DataAnnotations;
using MArchive.Domain.Base;

namespace MArchive.Domain.Lookup {
	public class ArchiveDO : LookupBaseDO {
		[StringLength ( 100 )]
		public string Path { get; set; }

        [Required]
        public int UserID { get; set; }
	}
}