using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Areas.Home.Models.MangaDb;
using Jikandesu.Areas.Manga.Models;
using Jikandesu.Models;
using Newtonsoft.Json;

namespace Jikandesu.Areas.Manga.Controllers
{
    public class MangaApiController : BaseApiController
    {
        private readonly IUserProvider _userProvider;
        private readonly IUserMangaProvider _userMangaProvider;
        private readonly IUserMangaSaver _userMangaSaver;
        private readonly IMangaPageProvider _mangaPageProvider;

        public MangaApiController(
            IUserProvider userProvider,
            IUserMangaProvider userMangaProvider,
            IUserMangaSaver userMangaSaver,
            IMangaPageProvider mangaPageProvider)
        {
            _userProvider = userProvider;
            _userMangaProvider = userMangaProvider;
            _userMangaSaver = userMangaSaver;
            _mangaPageProvider = mangaPageProvider;
        }

        [HttpPost]
        public async Task<ContentResult> GetMangaPage(string mangaUrl)
        {
            var user = _userProvider.GetUser(HttpContext);
            var page = await _mangaPageProvider.Get(mangaUrl, user);
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
                await _userMangaSaver.SaveUserPageLink(user, mangaPage);
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
                var mangaPages = await _mangaPageProvider.GetAll(user);
                return SuccessJsonContent(mangaPages);
            }
        }
    }
}