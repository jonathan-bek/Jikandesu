using System;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Jikandesu.Areas.Manga.Models.MangaDb;
using Jikandesu.Areas.Manga.Models.MangaParsers;
using Jikandesu.Services;

namespace Jikandesu.Areas.Manga.Models
{
    public class MangaPageParser : IMangaPageParser
    {
        private readonly IJdHttpService _http;
        private readonly IManganeloPageParser _manganeloPageParser;
        private readonly IMangaProvider _mangaProvider;
        private readonly IMangaSaver _mangaSaver;

        public MangaPageParser(IJdHttpService http,
            IManganeloPageParser manganeloPageParser,
            IMangaProvider mangaProvider,
            IMangaSaver mangaSaver)
        {
            _http = http;
            _manganeloPageParser = manganeloPageParser;
            _mangaProvider = mangaProvider;
            _mangaSaver = mangaSaver;
        }

        public async Task<MangaPage> ParseMangaPageHtml(string url)
        {
            var htmlString = await _http.AsyncGet(url);
            var html = ParseHtmlString(htmlString);
            var provider = GetMangaProvider(html);

            var page = await ParseMangaPageByProvider(html, provider, url);
            page.Id = await _mangaProvider.GetMangaPageId(url);
            if (page.Id == 0)
            {
                page.Id = await _mangaSaver.SaveMangaPage(page);
            }
            return page;
        }

        private async Task<MangaPage> ParseMangaPageByProvider(
            HtmlDocument html, MangaProviderEnum provider, string url)
        {
            MangaPage page;
            switch (provider)
            {
                case MangaProviderEnum.Manganelo:
                    page = await _manganeloPageParser.GetMangaDetails(html);
                    break;
                default:
                    throw new ArgumentException("Invalid url for manga page");
            }
            page.Url = url;
            return page;
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
    }
}
