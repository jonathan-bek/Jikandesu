using System.Collections.Generic;

namespace Jikandesu.Areas.Home.Models
{
    public class MangaPage
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Id { get; set; }
        public List<MangaChapter> MangaChapters { get; set; }
    }
}
