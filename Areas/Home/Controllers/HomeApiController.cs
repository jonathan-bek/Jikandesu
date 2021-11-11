using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Jikandesu.Areas.Authentication.Models;
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
        private readonly IUserProvider _userProvider;

        private const string baseUrl = "https://api.jikan.moe/v3";

        public HomeApiController(IJdCrud crud,
            IJdHttpService http,
            IMangaPageProvider pageProvider,
            IUserProvider userProvider)
        {
            _crud = crud;
            _http = http;
            _pageProvider = pageProvider;
            _userProvider = userProvider;
        }

        public void TestUser()
        {
            var user = _userProvider.GetUser(HttpContext);
            Trace.TraceInformation(user.UserId + "");
            Trace.TraceInformation(user.UserName);
            Trace.TraceInformation(user.Email);
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
            //var user = _userProvider.GetUser(HttpContext);
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
