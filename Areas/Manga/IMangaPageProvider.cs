using System.Collections.Generic;
using System.Threading.Tasks;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Areas.Manga.Models;

namespace Jikandesu.Areas.Manga
{
    public interface IMangaPageProvider
    {
        Task<MangaPage> Get(string mangaUrl, User user, bool fromUser = false);
        Task<List<MangaPage>> GetAll(User user);
    }
}