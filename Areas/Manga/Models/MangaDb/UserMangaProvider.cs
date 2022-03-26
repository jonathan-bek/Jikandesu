using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Services;

namespace Jikandesu.Areas.Home.Models.MangaDb
{
    public class UserMangaProvider : IUserMangaProvider
    {
        private readonly IJdCrud _crud;

        public UserMangaProvider(IJdCrud crud)
        {
            _crud = crud;
        }

        public async Task<List<LinkUserPage>> GetUserMangaLinks(User user)
        {
            using (_crud.GetOpenConnection())
            {
                var where = @"WHERE userId = @userId AND isManga = 1";
                var result = await _crud.GetListAsync<LinkUserPage>(
                    where, new { user.UserId });
                return result.ToList();
            }
        }

        public async Task<bool> UserMangaIsLinked(User user, string url)
        {
            if (user == null)
            {
                return false;
            }
            else
            {
                var link = await GetUserMangaLink(user, url);
                return link != null;
            }
        }

        public async Task<LinkUserPage> GetUserMangaLink(User user, string url)
        {
            using (_crud.GetOpenConnection())
            {
                var where = @"WHERE userId = @userId AND isManga = 1 AND url = @url";
                var result = await _crud.GetListAsync<LinkUserPage>(
                    where, new { user.UserId, url });
                return result.FirstOrDefault();
            }
        }

        public async Task<LinkUserPage> GetUserMangaLink(User user, int pageId)
        {
            using (_crud.GetOpenConnection())
            {
                var where = @"WHERE userId = @userId AND isManga = 1 AND pageId = @pageId";
                var result = await _crud.GetListAsync<LinkUserPage>(
                    where, new { user.UserId, pageId });
                return result.FirstOrDefault();
            }
        }
    }
}