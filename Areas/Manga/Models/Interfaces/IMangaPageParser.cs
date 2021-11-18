using System.Threading.Tasks;

namespace Jikandesu.Areas.Home.Models
{
    public interface IMangaPageParser
    {
        Task<MangaPage> ParseMangaPageHtml(string url);
    }
}
