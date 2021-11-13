using System.Collections.Generic;
using System.Threading.Tasks;
using Jikandesu.Areas.Authentication.Models;

namespace Jikandesu.Areas.Home.Models.MangaData
{
    public interface IUserMangaProvider
    {
        Task<List<LinkUserManga>> GetUserManga(User user);
        Task<List<LinkUserManga>> GetUserManga(User user, string mangaUrl);
    }
}