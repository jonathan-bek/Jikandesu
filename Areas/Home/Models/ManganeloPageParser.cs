using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Jikandesu.Areas.Home.Models
{
    public interface IManganeloPageParser
    {
        Task<MangaPage> GetMangaDetails(HtmlDocument html);
    }

    public class ManganeloPageParser : IManganeloPageParser
    {
        public async Task<MangaPage> GetMangaDetails(HtmlDocument html)
        {
            var title = GetManganeloTitle(html);
            var imgUrl = GetManganeloImageUrl(html);
            var id = GetManganeloId(html);
            var chapters = GetChapterList(html);
            return new MangaPage()
            {
                Title = title,
                ImageUrl = imgUrl,
                Id = id,
                MangaChapters = chapters
            };
        }

        private string GetManganeloTitle(HtmlDocument html)
        {
            var titleDiv = html.DocumentNode.SelectNodes("//div[@class='story-info-right']");
            var title = titleDiv.Descendants("h1").Single().InnerHtml;
            return title;
        }

        private string GetManganeloImageUrl(HtmlDocument html)
        {
            var imgDiv = html.DocumentNode.SelectNodes("//img[@class='img-loading']").Single();
            var imgSrc = imgDiv.Attributes.First(x => x.Name == "src").Value;
            return imgSrc;
        }

        private string GetManganeloId(HtmlDocument html)
        {
            var urlTag = html.DocumentNode.SelectNodes("//meta")
                .Where(x => x.GetAttributeValue("property", "").Contains("og:url")).First();
            var content = urlTag.GetAttributeValue("content", "");
            var id = content.Split('/').Last();
            return id;
        }

        private List<MangaChapter> GetChapterList(HtmlDocument html)
        {
            var result = new List<MangaChapter>();
            var list = html.DocumentNode.SelectNodes("//ul[@class='row-content-chapter']");
            var chapters = list.Descendants("li");
            foreach (var c in chapters)
            {
                var nameAndLink = c.Descendants(0).First(x => x.GetAttributeValue("class", "").Contains("chapter-name"));
                var uploadDate = c.Descendants(0).First(x => x.GetAttributeValue("class", "").Contains("chapter-time"))
                    .GetAttributeValue("title", "");
                var views = c.Descendants(0).First(x => x.GetAttributeValue("class", "").Contains("chapter-view"))
                    .InnerHtml;
                var chapter = new MangaChapter
                {
                    ChapterName = nameAndLink.InnerHtml,
                    ChapterUrl = nameAndLink.Attributes.First(x => x.Name == "href").Value,
                    ChapterUploadDate = DateTime.Parse(uploadDate),
                    ChapterViews = int.Parse(views, NumberStyles.AllowThousands)
                };
                result.Add(chapter);
            }

            return result;
        }
    }
}
