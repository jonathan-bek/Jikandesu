using System.Threading.Tasks;

namespace Jikandesu.Areas.Manga.Models.MangaDb
{
    public interface IMangaPageDataProvider
    {
        Task<int> GetMangaPageId(string mangaUrl);
    }
}