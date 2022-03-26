using System.Threading.Tasks;
using Jikandesu.Services;

namespace Jikandesu.Areas.Manga.Models.MangaDb
{
    public class MangaPageDataProvider : IMangaPageDataProvider
    {
        private readonly IJdCrud _crud;

        public MangaPageDataProvider(IJdCrud crud)
        {
            _crud = crud;
        }

        public async Task<int> GetMangaPageId(string url)
        {
            using (_crud.GetOpenConnection())
            {
                const string query = @"SELECT TOP 1 Id FROM tblMangaPage 
                                       WHERE pageUrl = @url";
                var id = await _crud.ExecuteScalarAsync<int>(query, new { url });
                return id;
            }
        }
    }
}