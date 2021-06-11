using System.Threading.Tasks;

namespace Jikandesu.Areas.Home.Models
{
    public interface IMangaPageProvider
    {
        Task<string> GetMangaPage(string url);
    }
}