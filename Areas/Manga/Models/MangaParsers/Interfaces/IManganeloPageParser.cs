using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Jikandesu.Areas.Manga.Models.MangaParsers
{
    public interface IManganeloPageParser
    {
        Task<MangaPage> GetMangaDetails(HtmlDocument html);
    }
}