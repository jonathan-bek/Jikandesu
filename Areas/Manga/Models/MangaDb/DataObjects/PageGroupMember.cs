using System;
using Dapper;

namespace Jikandesu.Areas.Manga.Models.MangaDb.DataObjects
{
    [Table("tblPageGroupMember")]
    public class PageGroupMember
    {
        [Column("pageGroupId")]
        public Guid PageGroupId { get; set; }
        [Column("pageId")]
        public int PageId { get; set; }
    }
}