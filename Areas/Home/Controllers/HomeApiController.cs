using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Areas.Home.Models;
using Jikandesu.Areas.Home.Models.MangaData;
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
        private readonly IUserMangaProvider _userMangaProvider;
        private readonly IUserMangaSaver _userMangaSaver;

        private const string baseUrl = "https://api.jikan.moe/v3";

        public HomeApiController(IJdCrud crud,
            IJdHttpService http,
            IMangaPageProvider pageProvider,
            IUserProvider userProvider,
            IUserMangaProvider userMangaProvider,
            IUserMangaSaver userMangaSaver)
        {
            _crud = crud;
            _http = http;
            _pageProvider = pageProvider;
            _userProvider = userProvider;
            _userMangaProvider = userMangaProvider;
            _userMangaSaver = userMangaSaver;
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
        [Authorize]
        public async Task<ContentResult> SaveMangaPage(string mangaPageStr)
        {
            var mangaPage = JsonConvert.DeserializeObject<MangaPage>(mangaPageStr);
            var user = _userProvider.GetUser(HttpContext);
            var userManga = await _userMangaProvider.GetUserManga(user, mangaPage.Url);
            var exists = userManga.Any();
            if (!exists)
            {
                await _userMangaSaver.SaveUserManga(user, mangaPage.Url);
            }
            return SuccessJsonContent(mangaPage);
        }
    }
}
