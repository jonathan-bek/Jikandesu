using System;
using Dapper;

namespace Jikandesu.Areas.Home.Models.MangaDb
{
    [Table("linkUserManga")]
    public class LinkUserManga
    {
        [Column("userId")]
        public Guid UserId { get; set; }
        [Column("mangaPageId")]
        public int MangaId { get; set; }

        [Column("mangaUrl")]
        public string MangaUrl { get; set; }
    }
}