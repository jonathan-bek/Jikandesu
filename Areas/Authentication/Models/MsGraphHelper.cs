using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Graph;

namespace Jikandesu.Areas.Authentication.Models
{
    public static class MsGraphHelper
    {
        public static async Task<CachedUser> GetUserDetails(string accessToken)
        {
            var client = new GraphServiceClient(
                new DelegateAuthenticationProvider(async (requestMessage) =>
                {
                    requestMessage.Headers.Authorization =
                        new AuthenticationHeaderValue("Bearer", accessToken);
                }));
            var user = await client.Me.Request()
                .Select(u => new { u.DisplayName, u.Mail, u.UserPrincipalName })
                .GetAsync();
            return new CachedUser
            {
                Avatar = string.Empty,
                DisplayName = user.DisplayName,
                Email = string.IsNullOrEmpty(user.Mail) ? user.UserPrincipalName : user.Mail
            };
        }
    }

    public class CachedUser
    {
        public string Avatar { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }
}