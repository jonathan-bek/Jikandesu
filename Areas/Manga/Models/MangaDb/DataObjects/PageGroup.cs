using System;
using System.Collections.Generic;
using Dapper;

namespace Jikandesu.Areas.Manga.Models.MangaDb.DataObjects
{
    [Table("tblPageGroup")]
    public class PageGroup
    {
        [Key]
        public Guid PageGroupId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("thumbnailImageUrl")]
        public string ThumbnailImageUrl { get; set; }

        [NotMapped]
        public List<PageGroupMember> PageGroupMembers { get; set; }

        public PageGroup()
        {
            PageGroupMembers = new List<PageGroupMember>();
        }
    }
}