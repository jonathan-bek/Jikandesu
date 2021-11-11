using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using Jikandesu.Areas.Authentication.Models;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

[assembly: OwinStartup(typeof(Jikandesu.App_Start.Startup))]

namespace Jikandesu.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    // Sets the client ID, authority, and redirect URI as obtained from Web.config
                    ClientId = ConfigurationManager.AppSettings.Get("ClientId"),
                    Authority = ConfigurationManager.AppSettings.Get("Authority"),
                    RedirectUri = Debugger.IsAttached
                        ? ConfigurationManager.AppSettings.Get("LocalRedirectUri")
                        : ConfigurationManager.AppSettings.Get("RedirectUri"),
                    // PostLogoutRedirectUri is the page that users will be redirected to after sign-out. In this case, it's using the home page
                    PostLogoutRedirectUri = Debugger.IsAttached
                        ? ConfigurationManager.AppSettings.Get("LocalRedirectUri")
                        : ConfigurationManager.AppSettings.Get("RedirectUri"),
                    Scope = OpenIdConnectScope.OpenIdProfile + " " + OpenIdConnectScope.Email,
                    // ResponseType is set to request the code id_token, which contains basic information about the signed-in user
                    ResponseType = OpenIdConnectResponseType.CodeIdToken,
                    // ValidateIssuer set to false to allow personal and work accounts from any organization to sign in to your application
                    // To only allow users from a single organization, set ValidateIssuer to true and the 'tenant' setting in Web.config to the tenant name
                    // To allow users from only a list of specific organizations, set ValidateIssuer to true and use the ValidIssuers parameter
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false // Simplification (see note below)
                    },
                    // OpenIdConnectAuthenticationNotifications configures OWIN to send notification of failed authentications to the OnAuthenticationFailed method
                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        AuthorizationCodeReceived = async n =>
                        {
                            Trace.TraceInformation("Login code received");
                            //await OnAuthorizationCodeReceivedAsync(n);
                        }
                        //AuthenticationFailed = async n =>
                        //{
                        //}
                    },
                    SaveTokens = true
                }
            );
        }

        //private static async Task OnAuthorizationCodeReceivedAsync(
        //    AuthorizationCodeReceivedNotification notification)
        //{
        //    Trace.TraceInformation("Code Received");
        //    var appId = ConfigurationManager.AppSettings.Get("ClientId");
        //    var idClient = ConfidentialClientApplicationBuilder.Create(appId)
        //        .WithRedirectUri(ConfigurationManager.AppSettings.Get("RedirectUri"))
        //        .WithClientSecret(ConfigurationManager.AppSettings.Get("ClientSecret"))
        //        .Build();
        //    string msg;
        //    try
        //    {
        //        var scopes = OpenIdConnectScope.OpenIdProfile.Split(' ');
        //        var result = await idClient.AcquireTokenByAuthorizationCode(scopes, notification.Code)
        //            .ExecuteAsync();
        //        msg = "User retrieved.";
        //    }
        //    catch (MsalException ex)
        //    {
        //        msg = "Token exception" + ex.Message;
        //    }
        //    Trace.TraceInformation(msg);
        //    notification.HandleResponse();
        //}
    }
}
