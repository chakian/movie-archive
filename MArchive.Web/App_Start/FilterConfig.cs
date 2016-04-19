using System.Web.Mvc;
using com.cagdaskorkut.mvc;

namespace MArchive.Web {
	public class FilterConfig {
		public static void RegisterGlobalFilters( GlobalFilterCollection filters ) {
			filters.Add( new HandleErrorCustomAttribute( ) );
		}
	}
}