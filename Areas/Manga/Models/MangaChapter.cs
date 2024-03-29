﻿using System;

namespace Jikandesu.Areas.Manga.Models
{
    public class MangaChapter
    {
        public string ChapterName { get; set; }
        public string ChapterUrl { get; set; }
        public DateTime ChapterUploadDate { get; set; }
        public int ChapterViews { get; set; }

        //Display
        public TimeSpan ChapterUploadedDateDifference { get; set; }
        public string ChapterUploadDateString { get; set; }
    }
}
