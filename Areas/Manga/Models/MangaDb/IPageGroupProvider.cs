using System.Collections.Generic;
using System.Threading.Tasks;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Areas.Manga.Models.MangaDb.DataObjects;

namespace Jikandesu.Areas.Manga.Models.MangaDb
{
    public interface IPageGroupProvider
    {
        Task<List<PageGroup>> GetPageGroups(User user);
    }
}