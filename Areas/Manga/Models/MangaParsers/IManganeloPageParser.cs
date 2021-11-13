using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Jikandesu.Areas.Home.Models
{
    public interface IManganeloPageParser
    {
        Task<MangaPage> GetMangaDetails(HtmlDocument html);
    }
}