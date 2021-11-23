using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Areas.Home.Models;
using Jikandesu.Areas.Home.Models.MangaData;
using Jikandesu.Models;
using Newtonsoft.Json;

namespace Jikandesu.Areas.Manga.Controllers
{
    public class MangaApiController : BaseApiController
    {
        private readonly IUserProvider _userProvider;
        private readonly IMangaPageParser _pageProvider;
        private readonly IUserMangaProvider _userMangaProvider;
        private readonly IUserMangaSaver _userMangaSaver;

        public MangaApiController(
            IUserProvider userProvider,
            IMangaPageParser pageProvider,
            IUserMangaProvider userMangaProvider,
            IUserMangaSaver userMangaSaver)
        {
            _userProvider = userProvider;
            _pageProvider = pageProvider;
            _userMangaProvider = userMangaProvider;
            _userMangaSaver = userMangaSaver;
        }

        [HttpPost]
        public async Task<ContentResult> GetMangaPage(string mangaUrl)
        {
            var page = await _pageProvider.ParseMangaPageHtml(mangaUrl);
            var user = _userProvider.GetUser(HttpContext);
            page.IsLinkedToUser = await _userMangaProvider.UserMangaIsLinked(user, page.Url);
            return SuccessJsonContent(page);
        }

        [HttpPost]
        [Authorize]
        public async Task<ContentResult> SaveMangaPage(string mangaPageStr)
        {
            var mangaPage = JsonConvert.DeserializeObject<MangaPage>(mangaPageStr);
            var user = _userProvider.GetUser(HttpContext);
            var isLinked = await _userMangaProvider.UserMangaIsLinked(user, mangaPage.Url);
            if (isLinked)
            {
                return SuccessJsonContent("Manga has already been saved.");
            }
            else
            {
                await _userMangaSaver.SaveUserMangaLink(user, mangaPage);
                return SuccessJsonContent("Manga successfully saved.");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ContentResult> GetUserManga()
        {
            var user = _userProvider.GetUser(HttpContext);
            if (user == null)
            {
                return SuccessJsonContent(new List<MangaPage>());
            }
            else
            {
                var mangaPages = await _userMangaProvider.GetUserManga(user);
                foreach (var p in mangaPages)
                {
                    var page = await _pageProvider.ParseMangaPageHtml(p.Url);
                    p.MangaChapters = page.MangaChapters;
                }
                return SuccessJsonContent(mangaPages);
            }
        }
    }
}