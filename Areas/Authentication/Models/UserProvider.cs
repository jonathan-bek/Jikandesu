using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Jikandesu.Areas.Authentication.Models
{
    public class UserProvider : IUserProvider
    {
        public User GetUser(HttpContextBase context)
        {
            var claimsIdentity = context.User.Identity as ClaimsIdentity;
            var id = claimsIdentity.FindFirst(idSchema).Value;
            var name = claimsIdentity.FindFirst("name").Value;
            var email = claimsIdentity.FindFirst(emailSchema).Value;
            var user = new User
            {
                UserId = new Guid(id),
                UserName = name,
                Email = email
            };
            return user;
        }
        private const string idSchema = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        private const string emailSchema = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
    }
}