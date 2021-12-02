using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Areas.Home.Models.MangaDb;
using Jikandesu.Areas.Manga.Models;
using Jikandesu.Areas.Manga.Models.MangaCache;

namespace Jikandesu.Areas.Manga
{
    public class MangaPageProvider : IMangaPageProvider
    {
        private readonly IMangaPageParser _pageParser;
        private readonly IUserMangaProvider _userMangaProvider;

        public MangaPageProvider(
            IMangaPageParser pageParser,
            IUserMangaProvider userMangaProvider)
        {
            _pageParser = pageParser;
            _userMangaProvider = userMangaProvider;
        }

        public async Task<MangaPage> Get(string mangaUrl, User user, bool fromUser = false)
        {
            var page = MangaCache.Get(mangaUrl);
            if (page == null)
            {
                page = await _pageParser.ParseMangaPageHtml(mangaUrl);
                MangaCache.Insert(page);
            }
            page.IsLinkedToUser = fromUser || await _userMangaProvider.UserMangaIsLinked(user, page.Url);
            return page;
        }

        public async Task<List<MangaPage>> GetAll(User user)
        {
            var result = new List<MangaPage>();
            var links = await _userMangaProvider.GetUserMangaLinks(user);
            foreach (var url in links.Select(x => x.MangaUrl))
            {
                var page = await Get(url, user, true);
                result.Add(page);
            }
            return result;
        }
    }
}