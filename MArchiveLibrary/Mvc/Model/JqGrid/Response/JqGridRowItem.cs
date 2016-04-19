using System.Runtime.Serialization;

namespace MArchiveLibrary.Mvc.Model.JqGrid.Response {
	[DataContract]
	public class JqGridRowItem {
		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public object id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public object[] cell { get; set; }
	}
}