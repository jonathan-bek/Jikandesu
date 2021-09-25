using System;

namespace Jikandesu.Areas.Home.Models
{
    public class MangaChapter
    {
        public string ChapterName { get; set; }
        public string ChapterUrl { get; set; }
        public DateTime ChapterUploadDate { get; set; }
        public int ChapterViews { get; set; }
    }
}
