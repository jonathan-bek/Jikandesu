using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Services;

namespace Jikandesu.Areas.Home.Models.MangaData
{
    public class UserMangaSaver : IUserMangaSaver
    {
        private readonly IJdCrud _crud;

        public UserMangaSaver(IJdCrud crud)
        {
            _crud = crud;
        }

        public async Task SaveUserManga(User user, string mangaUrl)
        {
            using (_crud.GetOpenConnection())
            {
                const string query =
                    @"INSERT INTO linkUserManga (userId, mangaUrl) 
                      VALUES (@userId, @mangaUrl)";
                var result = await _crud.ExecuteAsync(
                    query, new { user.UserId, mangaUrl });
                if (result == 0)
                {
                    throw new Exception("Failed to insert.");
                }
            }
        }
    }
}