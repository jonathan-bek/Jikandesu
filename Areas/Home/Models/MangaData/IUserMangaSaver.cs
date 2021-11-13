using System.Threading.Tasks;
using Jikandesu.Areas.Authentication.Models;

namespace Jikandesu.Areas.Home.Models.MangaData
{
    public interface IUserMangaSaver
    {
        Task<int> SaveUserManga(User user, string mangaUrl);
    }
}