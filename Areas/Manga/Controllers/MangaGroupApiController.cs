using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Areas.Manga.Models.MangaDb;
using Jikandesu.Models;

namespace Jikandesu.Areas.Manga.Controllers
{
    public class MangaGroupApiController : BaseApiController
    {
        private readonly IUserProvider _userProvider;
        private readonly IPageGroupProvider _pageGroupProvider;

        public MangaGroupApiController(
            IUserProvider userProvider,
            IPageGroupProvider pageGroupProvider)
        {
            _userProvider = userProvider;
            _pageGroupProvider = pageGroupProvider;
        }

        public async Task<ContentResult> GetMangaGroups()
        {
            var user = _userProvider.GetUser(HttpContext);
            var pageGroups = await _pageGroupProvider.GetPageGroups(user);
            return SuccessJsonContent(pageGroups);
        }
    }
}