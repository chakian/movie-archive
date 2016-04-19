using System;

namespace MArchive.Domain.Base {
	public class AuditableDO : BaseDO {
		public int InsertUserID { get; set; }
		public DateTime InsertDate { get; set; }

		public int UpdateUserID { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}