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
            //GetUserDetails(); 
            var name = ClaimsPrincipal.Current.Identity.Name;
            Trace.TraceInformation("User name: " + name);
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            HttpContext.GetOwinContext().Authentication.Challenge(CookieAuthenticationDefaults.AuthenticationType);
            var claims = claimsIdentity.Claims;
            foreach (var c in claims)
            {
                Trace.TraceInformation("type: " + c.Type);
                Trace.TraceInformation("issuer: " + c.Issuer);
                Trace.TraceInformation("value: " + c.Value);
                foreach (var p in c.Properties)
                {
                    Trace.TraceInformation("propKey: " + p.Key);
                }
            }
            //Response.Redirect("https://localhost:44384");
            Response.Redirect("/");
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
