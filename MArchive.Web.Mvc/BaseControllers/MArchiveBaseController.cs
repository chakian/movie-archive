using System.Configuration;
using MArchive.Web.Mvc.Attributes;
using com.cagdaskorkut.mvc;

namespace MArchive.Web.Mvc.BaseControllers {
    [MArchiveAuthorizeUser]
    public class MArchiveBaseController : BaseController {
        protected string GetFullPathForImage(string imageName) {
            return Url.Action(imageName, ConfigurationManager.AppSettings["relativeWebSiteImageController"]);
        }

        public void SetSearchKeyword(string keyword) {
            WebSession["SearchKeyword"] = keyword;
        }
        public string GetSearchKeyword() {
            return (WebSession["SearchKeyword"] == null ? null : WebSession["SearchKeyword"].ToString());
        }
    }
}
