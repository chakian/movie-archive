using MArchiveLibrary.Attributes;
using System.Web.Mvc;

namespace MArchive.Web {
	public class FilterConfig {
		public static void RegisterGlobalFilters( GlobalFilterCollection filters ) {
			filters.Add( new HandleErrorCustomAttribute( ) );
		}
	}
}