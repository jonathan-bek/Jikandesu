using System.Collections.Generic;
using Dapper;
using Jikandesu.Areas.Manga.Models.MangaParsers;

namespace Jikandesu.Areas.Home.Models
{
    [Table("tblMangaPage")]
    public class MangaPage
    {
        [Key]
        public int Id { get; set; }
        [Column("pageUrl")]
        public string Url { get; set; }
        [Column("pageTitle")]
        public string Title { get; set; }
        [Column("imageUrl")]
        public string ImageUrl { get; set; }
        [Column("mangaProviderId")]
        public MangaProviderEnum MangaProviderId { get; set; }
        [NotMapped]
        public List<MangaChapter> MangaChapters { get; set; }
        [NotMapped]
        public bool IsLinkedToUser { get; set; }
    }
}
