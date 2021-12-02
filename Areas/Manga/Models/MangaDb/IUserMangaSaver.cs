using System.Threading.Tasks;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Areas.Manga.Models;

namespace Jikandesu.Areas.Home.Models.MangaDb
{
    public interface IUserMangaSaver
    {
        Task SaveUserMangaLink(User user, MangaPage mangaPage);
    }
}