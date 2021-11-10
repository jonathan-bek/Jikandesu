using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Jikandesu.Areas.Home.Models;
using Jikandesu.Models;
using Jikandesu.Services;
using Microsoft.Owin.Security.Cookies;
using Newtonsoft.Json;

namespace Jikandesu.Areas.Home.Controllers
{
    public class HomeApiController : BaseApiController
    {
        private readonly IJdCrud _crud;
        private readonly IJdHttpService _http;
        private readonly IMangaPageProvider _pageProvider;

        private const string baseUrl = "https://api.jikan.moe/v3";

        public HomeApiController(IJdCrud crud,
            IJdHttpService http,
            IMangaPageProvider pageProvider)
        {
            _crud = crud;
            _http = http;
            _pageProvider = pageProvider;
        }

        public void TestUser()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            HttpContext.GetOwinContext().Authentication.Challenge(CookieAuthenticationDefaults.AuthenticationType);
            var id = claimsIdentity.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            var name = claimsIdentity.FindFirst("name").Value;
            var email = claimsIdentity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            var user = new User
            {
                UserId = new Guid(id),
                UserName = name,
                Email = email
            };
            Trace.TraceInformation(user.UserId + "");
            Trace.TraceInformation(user.UserName);
            Trace.TraceInformation(user.Email);
            Response.Redirect("/");
        }
        public class User
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
        }

        [HttpPost]
        public async Task<ContentResult> GetMangaPage(string mangaUrl)
        {
            var page = await _pageProvider.GetMangaPage(mangaUrl);
            return SuccessJsonContent(page);
        }

        [HttpPost]
        public async Task<ContentResult> SaveMangaPage(string mangaPageStr)
        {
            var mangaPage = JsonConvert.DeserializeObject<MangaPage>(mangaPageStr);

            using (_crud.GetOpenConnection())
            {
                var query = @"INSERT INTO linkUserManga (userId, mangaId) VALUES (NEWID(), @mangaId)";
                var test = await _crud.ExecuteAsync(query, new { mangaId = mangaPage.Id });
            }
            return SuccessJsonContent(mangaPage);
        }

        public async Task<ContentResult> GetInfoFromDb()
        {
            var query = @"SELECT randominteger AS ID FROM testTable";
            using (_crud.GetOpenConnection())
            {
                var result = await _crud.GetAsync<TestClass>(1);
                return SuccessJsonContent(result);
            }
        }

        [Table("testTable")]
        public class TestClass
        {
            [Column("randominteger")]
            public int ID { get; set; }
        }
    }
}
