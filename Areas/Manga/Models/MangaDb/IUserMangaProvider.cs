using System.Collections.Generic;
using System.Threading.Tasks;
using Jikandesu.Areas.Authentication.Models;

namespace Jikandesu.Areas.Home.Models.MangaData
{
    public interface IUserMangaProvider
    {
        Task<List<MangaPage>> GetUserManga(User user);
        Task<List<LinkUserManga>> GetUserMangaLinks(User user);
        Task<LinkUserManga> GetUserMangaLink(User user, string mangaUrl);
    }
}