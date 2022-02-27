using System;
using Dapper;

namespace Jikandesu.Areas.Manga.Models.MangaDb.DataObjects
{
    [Table("linkUserPageGroup")]
    public class LinkUserPageGroup
    {
        [Column("userId")]
        public Guid UserId { get; set; }
        [Column("pageGroupId")]
        public Guid PageGroupId { get; set; }
    }
}