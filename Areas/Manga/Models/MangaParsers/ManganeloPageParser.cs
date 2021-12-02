using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Jikandesu.Areas.Manga.Models.MangaParsers
{
    public class ManganeloPageParser : IManganeloPageParser
    {
        public async Task<MangaPage> GetMangaDetails(HtmlDocument html)
        {
            var title = GetManganeloTitle(html);
            var imgUrl = GetManganeloImageUrl(html);
            var chapters = GetChapterList(html);
            AddChapterDateDisplayInfo(chapters);
            return new MangaPage()
            {
                Title = title,
                ImageUrl = imgUrl,
                MangaProviderId = MangaProviderEnum.Manganelo,
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

        private List<MangaChapter> GetChapterList(HtmlDocument html)
        {
            var result = new List<MangaChapter>();
            var list = html.DocumentNode.SelectNodes("//ul[@class='row-content-chapter']");
            var chapters = list.Descendants("li");
            foreach (var c in chapters)
            {
                var chapter = new MangaChapter
                {
                    ChapterName = GetChapterName(c),
                    ChapterUrl = GetChapterUrl(c),
                    ChapterUploadDate = GetChapterUploadDate(c),
                    ChapterViews = GetChapterViews(c)
                };
                result.Add(chapter);
            }

            return result;
        }

        private string GetChapterName(HtmlNode chapterNode)
        {
            var link = chapterNode.Descendants(0)
                .First(x => x.GetAttributeValue("class", "").Contains("chapter-name"));
            return link.InnerHtml;
        }

        private string GetChapterUrl(HtmlNode chapterNode)
        {
            var link = chapterNode.Descendants(0)
                .First(x => x.GetAttributeValue("class", "").Contains("chapter-name"));
            return link.GetAttributeValue("href", "");
        }

        private DateTime GetChapterUploadDate(HtmlNode chapterNode)
        {
            var uploadDate = chapterNode.Descendants(0)
                .First(x => x.GetAttributeValue("class", "").Contains("chapter-time"));
            return DateTime.Parse(uploadDate.GetAttributeValue("title", ""));
        }

        private int GetChapterViews(HtmlNode chapterNode)
        {
            var views = chapterNode.Descendants(0)
                .First(x => x.GetAttributeValue("class", "").Contains("chapter-view"));
            return int.Parse(views.InnerHtml, NumberStyles.AllowThousands);
        }

        private void AddChapterDateDisplayInfo(List<MangaChapter> chapters)
        {
            chapters.ForEach(c =>
            {
                c.ChapterUploadedDateDifference = DateTime.Now - c.ChapterUploadDate;
                c.ChapterUploadDateString = c.ChapterUploadedDateDifference.Days > 30
                    ? c.ChapterUploadDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                    : c.ChapterUploadedDateDifference.Days > 1
                        ? $"{c.ChapterUploadedDateDifference.Days} days ago"
                        : $"{c.ChapterUploadedDateDifference.Hours} hours ago";
            });
        }
    }
}
