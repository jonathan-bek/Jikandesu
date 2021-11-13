using System;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Jikandesu.Services;

namespace Jikandesu.Areas.Home.Models
{
    public class MangaPageProvider : IMangaPageProvider
    {
        private readonly IJdHttpService _http;
        private readonly IManganeloPageParser _manganeloPageParser;

        public MangaPageProvider(IJdHttpService http,
            IManganeloPageParser manganeloPageParser)
        {
            _http = http;
            _manganeloPageParser = manganeloPageParser;
        }

        public async Task<MangaPage> GetMangaPage(string url)
        {
            var htmlString = await _http.AsyncGet(url);
            var html = ParseHtmlString(htmlString);
            var provider = GetMangaProvider(html);
            if (provider == MangaProviderEnum.Manganelo)
            {
                return await _manganeloPageParser.GetMangaDetails(html);
            }

            throw new ArgumentException("Invalid url for manga page", nameof(url));
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
