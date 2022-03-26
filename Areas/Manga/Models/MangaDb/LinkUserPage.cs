using System;
using Dapper;

namespace Jikandesu.Areas.Home.Models.MangaDb
{
    [Table("linkUserPage")]
    public class LinkUserPage
    {
        [Column("userId")]
        public Guid UserId { get; set; }
        [Column("pageId")]
        public int PageId { get; set; }

        [Column("url")]
        public string Url { get; set; }
        [Column("isManga")]
        public bool IsManga { get; set; }
    }
}