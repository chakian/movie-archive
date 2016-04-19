using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using com.cagdaskorkut.utility.Exceptions;
using MArchive.BL;
using MArchive.Web.Models.Account;
using MArchive.Web.Mvc.Attributes;
using MArchive.Web.Mvc.BaseControllers;
using MArchive.Domain.User;
using com.cagdaskorkut.mvc;

namespace MArchive.Web.Controllers {
    [Role(RoleEnum.SKIP_AUTHORIZATION)]
	public class AccountController : MArchiveBaseController {
		public ActionResult Login( ) {
			if( User.Identity.IsAuthenticated ) {
				return Redirect( FormsAuthentication.DefaultUrl );
			}
			return View( );
		}

		[HttpPost]
		public ActionResult Login( LoginViewModel model, string returnUrl ) {
			if( ModelState.IsValid ) {
				try {
					UserDO user = UserBL.GetUserDOByUsername( model.Username );

					if( UserBL.IsUserAuthenticationCorrect( user, model.Username, model.Password ) ){
						EmptySession( );
						FormsAuthentication.SetAuthCookie( model.Username, model.RememberMe );
						Session.Add( "User", user );

						if( Url.IsLocalUrl( returnUrl ) && returnUrl.Length > 1 && returnUrl.StartsWith( "/" ) && !returnUrl.StartsWith( "//" ) && !returnUrl.StartsWith( "/\\" ) ) {
							return Redirect( returnUrl );
						} else {
							return RedirectToAction( "Index", "Home" );
						}
					}
				} catch( BusinessException ex ) {
					ModelState.AddModelError( "LogonError", ex.Message );
				}
			}

			return View( );
		}

		public ActionResult ShowAccount( ) {
			if( User.Identity.IsAuthenticated ) {
				//TODO: Account sayfası yapabiliriz
				return Redirect( FormsAuthentication.DefaultUrl );
			}
			return RedirectToAction( "Login" );
		}

		public ActionResult Logout( string returnUrl ) {
			FormsAuthentication.SignOut( );
			EmptySession( );

			if( Url.IsLocalUrl( returnUrl ) && returnUrl.Length > 1 && returnUrl.StartsWith( "/" ) && !returnUrl.StartsWith( "//" ) && !returnUrl.StartsWith( "/\\" ) ) {
				return Redirect( returnUrl );
			} else {
				return RedirectToAction( "Index", "Home" );
			}
		}

		private void EmptySession( ) {
			IList<string> sessionKeys = new List<string>( );
			foreach( string key in Session.Keys ) {
				sessionKeys.Add( key );
			}
			foreach( string key in sessionKeys ) {
				Session[key] = null;
			}
		}
	}
}