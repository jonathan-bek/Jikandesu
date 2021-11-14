using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Services;

namespace Jikandesu.Areas.Home.Models.MangaData
{
    public class UserMangaProvider : IUserMangaProvider
    {
        private readonly IJdCrud _crud;

        public UserMangaProvider(IJdCrud crud)
        {
            _crud = crud;
        }

        public async Task<List<MangaPage>> GetUserManga(User user)
        {
            var links = await GetUserMangaLinks(user);
            var mangaPageIds = links.Select(x => x.MangaId);
            using (_crud.GetOpenConnection())
            {
                var where = @"WHERE Id IN @mangaPageIds";
                var result = await _crud.GetListAsync<MangaPage>(
                    where, new { mangaPageIds });
                return result.ToList();
            }
        }

        public async Task<List<LinkUserManga>> GetUserMangaLinks(User user)
        {
            using (_crud.GetOpenConnection())
            {
                var where = @"WHERE userId = @userId";
                var result = await _crud.GetListAsync<LinkUserManga>(
                    where, new { user.UserId });
                return result.ToList();
            }
        }

        public async Task<List<LinkUserManga>> GetUserMangaLinks(User user, string mangaUrl)
        {
            using (_crud.GetOpenConnection())
            {
                var where = @"WHERE userId = @userId AND mangaUrl = @mangaUrl";
                var result = await _crud.GetListAsync<LinkUserManga>(
                    where, new { user.UserId, mangaUrl });
                return result.ToList();
            }
        }
    }
}