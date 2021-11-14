using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Areas.Home.Models;
using Jikandesu.Areas.Home.Models.MangaData;
using Jikandesu.Models;
using Newtonsoft.Json;

namespace Jikandesu.Areas.Home.Controllers
{
    public class HomeApiController : BaseApiController
    {
        private readonly IMangaPageProvider _pageProvider;
        private readonly IUserProvider _userProvider;
        private readonly IUserMangaProvider _userMangaProvider;
        private readonly IUserMangaSaver _userMangaSaver;

        public HomeApiController(
            IMangaPageProvider pageProvider,
            IUserProvider userProvider,
            IUserMangaProvider userMangaProvider,
            IUserMangaSaver userMangaSaver)
        {
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
            var userManga = await _userMangaProvider.GetUserMangaLinks(user, mangaPage.Url);
            var exists = userManga.Any();
            if (exists)
            {
                return SuccessJsonContent("Manga has already been saved.");
            }
            else
            {
                await _userMangaSaver.SaveUserMangaLink(user, mangaPage);
                return SuccessJsonContent("Manga successfully saved.");
            }
        }
    }
}
