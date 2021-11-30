using System;
using System.Web.Caching;
using Jikandesu.Areas.Home.Models;

namespace Jikandesu.Areas.Manga.Models.MangaCache
{
    public static class MangaCache
    {
        public static Cache _cache = new Cache();
        public static object _lockObject = new object();

        public static MangaPage Get(string url)
        {
            var result = _cache.Get(url);
            return result == null ? null : (MangaPage) result;
        }

        public static void Insert(MangaPage mangaPage)
        {
            var url = mangaPage.Url;
            lock (_lockObject)
            {
                var exists = Get(url);
                if (exists == null)
                {
                    _cache.Insert(url, mangaPage,
                        null,
                        DateTime.Now.AddMinutes(5),
                        Cache.NoSlidingExpiration);
                }
            }
        }
    }
}