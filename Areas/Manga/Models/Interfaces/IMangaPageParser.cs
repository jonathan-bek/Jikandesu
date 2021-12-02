using System.Threading.Tasks;

namespace Jikandesu.Areas.Manga.Models
{
    public interface IMangaPageParser
    {
        Task<MangaPage> ParseMangaPageHtml(string url);
    }
}
