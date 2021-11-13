using System.Configuration;
using System.Web;
using System.Web.Mvc;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Models;
using Microsoft.Owin.Security.Cookies;

namespace Jikandesu.Areas.Home.Controllers
{
    public class AuthApiController : BaseApiController
    {
        private IUserProvider _userProvider;

        public AuthApiController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        [HttpGet]
        [Authorize]
        public void ManualLogin()
        {
            //HttpContext.GetOwinContext().Authentication.Challenge(CookieAuthenticationDefaults.AuthenticationType);
            var user = _userProvider.GetUser(HttpContext);
            Response.Redirect("/");
        }

        [HttpGet]
        public void ManualLogout()
        {
            var authmgr = HttpContext.GetOwinContext().Authentication;
            authmgr.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            Response.Redirect("/");
        }

        [HttpGet]
        public void Logout()
        {
            var url = ConfigurationManager.AppSettings.Get("LogoutUri");
            Response.Redirect(url);
        }
    }
}