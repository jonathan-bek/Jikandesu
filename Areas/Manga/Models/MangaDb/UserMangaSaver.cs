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

        public async Task SaveUserMangaLink(User user, MangaPage mangaPage)
        {
            const string query = @"
                INSERT INTO linkUserManga 
                (userId, mangaPageId, mangaUrl) 
                VALUES (@userId, @id, @url)";
            using (_crud.GetOpenConnection())
            {
                var result = await _crud.ExecuteAsync(query,
                    new { user.UserId, mangaPage.Id, mangaPage.Url });
                if (result != 1)
                {
                    throw new Exception("Failed to insert.");
                }
            }
        }
    }
}