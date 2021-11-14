using System.Threading.Tasks;
using Jikandesu.Areas.Home.Models;

namespace Jikandesu.Areas.Manga.Models.MangaDb
{
    public interface IMangaSaver
    {
        Task<int> SaveMangaPage(MangaPage page);
    }
}