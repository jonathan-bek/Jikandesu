using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Jikandesu.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

namespace Jikandesu.Areas.Home.Controllers
{
    public class AuthApiController: BaseApiController

    {
        [HttpGet]
        public void ManualLogin()
        {
            if (ConfigurationManager.AppSettings.Get("EnableLocalLogin") == "true")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "LocalDev"),
                    new Claim(ClaimTypes.NameIdentifier, "LocalDev"),
                    new Claim(ClaimTypes.Email, "LocalDev@local.com"),
                    new Claim(ClaimTypes.Surname, "Developer"),
                    new Claim(ClaimTypes.GivenName, "LocalDev"),
                    new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity")
                };
                var authmgr = HttpContext.GetOwinContext().Authentication;
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);
                authmgr.SignIn(new AuthenticationProperties { IsPersistent = false, AllowRefresh = true }, claimsIdentity);
            }
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