using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Jikandesu.Services;

namespace Jikandesu.Areas.Home.Models
{
    public class MangaPageProvider : IMangaPageProvider
    {
        private readonly IJdHttpService _http;

        public MangaPageProvider(IJdHttpService http)
        {
            _http = http;
        }

        public async Task<string> GetMangaPage(string url)
        {
            var htmlString = await _http.AsyncGet(url);
            var html = ParseHtmlString(htmlString);
            var provider = GetMangaProvider(html);
            if (provider == MangaProviderEnum.Manganelo)
            {
                var title = GetManganeloTitle(html);
                var id = GetManganeloId(html);
                GetChapterList(html);
                return id;
            }

            return htmlString;
        }

        private void GetChapterList(HtmlDocument html)
        {
            var chapterDiv = html.DocumentNode.SelectNodes("//ul[@class='row-content-chapter']");
            var chapters = chapterDiv.Descendants("li");
        }

        private string GetManganeloTitle(HtmlDocument html)
        {
            var titleDiv = html.DocumentNode.SelectNodes("//div[@class='story-info-right']");
            var title = titleDiv.Descendants("h1").Single().InnerHtml;
            return title;
        }

        private string GetManganeloId(HtmlDocument html)
        {
            var urlTag = html.DocumentNode.SelectNodes("//meta")
                .Where(x => x.GetAttributeValue("property", "").Contains("og:url")).First();
            var content = urlTag.GetAttributeValue("content", "");
            var id = content.Split('/').Last();
            return id;
        }

        private HtmlDocument ParseHtmlString(string htmlString)
        {
            var html = new HtmlDocument();
            html.LoadHtml(htmlString);
            return html;
        }

        private MangaProviderEnum GetMangaProvider(HtmlDocument html)
        {
            //check manganelo
            var manganeloSiteTag = html.DocumentNode.SelectNodes("//meta")
                .Where(x => x.GetAttributeValue("property", "").Contains("og:site_name")
                    && x.GetAttributeValue("content", "").ToUpperInvariant().Contains("MANGANELO"));
            return manganeloSiteTag == null
                ? MangaProviderEnum.Invalid
                : MangaProviderEnum.Manganelo;
        }

        private enum MangaProviderEnum
        {
            Invalid,
            Manganelo
        }
    }
}
