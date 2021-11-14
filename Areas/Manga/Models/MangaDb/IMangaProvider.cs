using System.Threading.Tasks;

namespace Jikandesu.Areas.Manga.Models.MangaDb
{
    public interface IMangaProvider
    {
        Task<int> GetMangaPageId(string mangaUrl);
    }
}