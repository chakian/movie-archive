using System.Collections.Generic;
using System.Web.Mvc;

namespace MArchiveLibrary.JqGrid.Model
{
	//Requires custom model binder, because it comes as a JSON string inside POST parameter
	[ModelBinder( typeof( JqGridModelBinder ) )]
	public sealed class JqGridFilters {
		#region Properties

		public string groupOp { get; set; }

		public List<JqGridFilterRule> rules { get; set; }

		#endregion Properties

		#region Constructor

		public JqGridFilters( ) {
			groupOp = "AND";
		}

		#endregion Constructor
	}
}