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

namespace Jikandesu.Areas.Manga.Controllers
{
    public class MangaApiController : BaseApiController
    {
        private readonly IUserProvider _userProvider;
        private readonly IUserMangaProvider _userMangaProvider;

        public MangaApiController(
            IUserProvider userProvider,
            IUserMangaProvider userMangaProvider)
        {
            _userProvider = userProvider;
            _userMangaProvider = userMangaProvider;
        }

        [HttpGet]
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
                return SuccessJsonContent(mangaPages);
            }
        }
    }
}