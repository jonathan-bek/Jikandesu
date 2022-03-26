using System;
using System.Threading.Tasks;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Areas.Manga.Models;
using Jikandesu.Services;

namespace Jikandesu.Areas.Home.Models.MangaDb
{
    public class UserMangaSaver : IUserMangaSaver
    {
        private readonly IJdCrud _crud;

        public UserMangaSaver(IJdCrud crud)
        {
            _crud = crud;
        }

        public async Task SaveUserPageLink(User user, MangaPage page)
        {
            const string query = @"
                INSERT INTO linkUserPage 
                (userId, pageId, url, isManga) 
                VALUES (@userId, @id, @url, 1)";
            using (_crud.GetOpenConnection())
            {
                var result = await _crud.ExecuteAsync(query,
                    new { user.UserId, page.Id, page.Url });
                if (result != 1)
                {
                    throw new Exception("Failed to insert.");
                }
            }
        }
    }
}