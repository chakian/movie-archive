using System.Web.Mvc;
using System.Web.WebPages;

namespace MArchive.Web.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static HtmlHelper GetMvcHtmlHelper(this System.Web.WebPages.Html.HtmlHelper html)
        {
            return ((System.Web.Mvc.WebViewPage)WebPageContext.Current.Page).Html;
        }

        public static UrlHelper GetUrlHelper()
        {
            return ((System.Web.Mvc.WebViewPage)WebPageContext.Current.Page).Url;
        }
    }
}