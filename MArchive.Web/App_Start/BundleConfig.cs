using System.Web;
using System.Web.Optimization;

namespace MArchive.Web {
	public class BundleConfig {
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles( BundleCollection bundles ) {
			bundles.Add( new ScriptBundle( "~/bundles/jquery" ).Include(
						"~/Scripts/jquery-{version}.js",
						"~/Scripts/jquery.form.js" ) );

			bundles.Add( new ScriptBundle( "~/bundles/jqueryui" ).Include(
						"~/Scripts/jquery-ui-{version}.js" ) );

			bundles.Add( new ScriptBundle( "~/bundles/jqueryval" ).Include(
						"~/Scripts/jquery.unobtrusive*",
						"~/Scripts/jquery.validate*" ) );

			bundles.Add( new ScriptBundle( "~/bundles/jqgrid" ).Include(
						"~/Content/jqgrid/js/i18n/grid.locale-en.js",
						"~/Content/jqgrid/js/jquery.jqGrid.src.js",
						"~/Scripts/jqgridHelper.js" ) );

			bundles.Add( new ScriptBundle( "~/bundles/bootstrap" ).Include( "~/Content/bootstrap/js/bootstrap.js" ) );

			bundles.Add( new ScriptBundle( "~/bundles/site/js" ).Include(
						"~/Scripts/site.js" ) );

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add( new ScriptBundle( "~/bundles/modernizr" ).Include(
						"~/Scripts/modernizr-*" ) );

			bundles.Add( new StyleBundle( "~/Content/jqgrid/css" ).Include( "~/Content/jqgrid/css/ui.jqgrid.css" ) );

			bundles.Add( new StyleBundle( "~/Content/andia/css" ).Include( "~/Content/css/font-awesome.css", "~/Content/css/andiaStyle.css" ) );

			bundles.Add( new StyleBundle( "~/Content/css" ).Include( "~/Content/css/site.css" ) );

			bundles.Add( new StyleBundle( "~/Content/bootstrap/css" ).Include(
						"~/Content/bootstrap/css/bootstrap.css",
						"~/Content/bootstrap/css/bootstrap-responsive.css" ) );

			bundles.Add( new StyleBundle( "~/Content/themes/base/css" ).Include(
						"~/Content/themes/base/jquery-ui.css",
						"~/Content/themes/base/jquery.ui.core.css",
						"~/Content/themes/base/jquery.ui.resizable.css",
						"~/Content/themes/base/jquery.ui.selectable.css",
						"~/Content/themes/base/jquery.ui.accordion.css",
						"~/Content/themes/base/jquery.ui.autocomplete.css",
						"~/Content/themes/base/jquery.ui.button.css",
						"~/Content/themes/base/jquery.ui.dialog.css",
						"~/Content/themes/base/jquery.ui.slider.css",
						"~/Content/themes/base/jquery.ui.tabs.css",
						"~/Content/themes/base/jquery.ui.datepicker.css",
						"~/Content/themes/base/jquery.ui.progressbar.css",
						"~/Content/themes/base/jquery.ui.theme.css" ) );
		}
	}
}