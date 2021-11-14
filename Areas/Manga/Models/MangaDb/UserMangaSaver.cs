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

        public async Task<int> SaveUserMangaLink(User user, MangaPage mangaPage)
        {
            using (_crud.GetOpenConnection())
            {
                var toInsert = new LinkUserManga
                {
                    UserId = user.UserId,
                    MangaId = mangaPage.Id,
                    MangaUrl = mangaPage.Url
                };
                var result = await _crud.InsertAsync<LinkUserManga>(toInsert);
                return result ?? throw new Exception("Failed to insert.");
            }
        }
    }
}