using System.Web.Mvc;

namespace MArchive.Web.Base {
	[OutputCache( NoStore = true, Duration = 0, VaryByParam = "*" )]
	public class WebViewPageBase<T> : WebViewPage<T> {
		//public FileBasedLocalizationManager Localizer { get; set; }

		public override void InitHelpers( ) {
			base.InitHelpers( );

		}

		public override void Execute( ) {

		}

		public string SuccessMessage { get { return TempData["Success"] == null ? string.Empty : TempData["Success"].ToString( ); } }
		public string ErrorMessage { get { return TempData["Error"] == null ? string.Empty : TempData["Error"].ToString( ); } }
	}
}