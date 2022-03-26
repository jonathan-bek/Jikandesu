using System.Collections.Generic;
using System.Threading.Tasks;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Areas.Manga.Models;

namespace Jikandesu.Areas.Home.Models.MangaDb
{
    public interface IUserMangaProvider
    {
        Task<List<LinkUserPage>> GetUserMangaLinks(User user);
        Task<bool> UserMangaIsLinked(User user, string mangaUrl);
        Task<LinkUserPage> GetUserMangaLink(User user, string mangaUrl);
        Task<LinkUserPage> GetUserMangaLink(User user, int mangaId);
    }
}