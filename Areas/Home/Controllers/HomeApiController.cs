using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Jikandesu.Areas.Home.Models;
using Jikandesu.Areas.Home.Models.JsonModels;
using Jikandesu.Models;
using Jikandesu.Services;
using Newtonsoft.Json;

namespace Jikandesu.Areas.Home.Controllers
{
    public class HomeApiController : BaseApiController
    {
        private readonly IJdCrud _crud;
        private readonly IJdHttpService _http;
        private readonly IMangaPageProvider _pageProvider;

        private const string baseUrl = "https://api.jikan.moe/v3";

        public HomeApiController(IJdCrud crud,
            IJdHttpService http,
            IMangaPageProvider pageProvider)
        {
            _crud = crud;
            _http = http;
            _pageProvider = pageProvider;
        }

        [HttpPost]
        public async Task<ContentResult> GetMangaPage(string mangaUrl)
        {
            var page = await _pageProvider.GetMangaPage(mangaUrl);
            return SuccessJsonContent(page);
        }

        [HttpPost]
        public async Task<ContentResult> SaveMangaPage(string mangaPageStr)
        {
            var mangaPage = JsonConvert.DeserializeObject<MangaPage>(mangaPageStr);

            using (_crud.GetOpenConnection())
            {
                var query = @"INSERT INTO linkUserManga (userId, mangaId) VALUES (NEWID(), @mangaId)";
                var test = await _crud.ExecuteAsync(query, new { mangaId = mangaPage.Id });
            }
            return SuccessJsonContent(mangaPage);
        }

        [HttpGet]
        public void Logout()
        {
            var url = ConfigurationManager.AppSettings.Get("logoutUri");
            Response.Redirect(url);
        }

        [HttpPost]
        public async Task<ContentResult> LoadSearchResults(
            IEnumerable<SearchFilter> filterCollection)
        {
            var animeFilter = filterCollection.First(x => x.SearchCategory.Equals(SearchCategoryEnum.Anime));
            var animeResult = await _http.AsyncGet($"{baseUrl}/search/{CreateSearchQueryString(animeFilter)}&page=1");
            var mangaFilter = filterCollection.First(x => x.SearchCategory.Equals(SearchCategoryEnum.Manga));
            var mangaResult = await _http.AsyncGet($"{baseUrl}/search/{CreateSearchQueryString(mangaFilter)}&page=1");

            var result1 = JsonConvert.DeserializeObject<SearchResults>(animeResult).Results;
            var result2 = JsonConvert.DeserializeObject<SearchResults>(mangaResult).Results;

            return SuccessJsonContent(new { AnimeResult = result1, MangaResult = result2 });
        }

        private string CreateSearchQueryString(SearchFilter filter)
        {
            return $"{filter.SearchCategory}?q={filter.Name}";
        }

        [HttpGet]
        public async Task<ContentResult> LoadCurrentSeasonAnime()
        {
            var url = $"{baseUrl}/season";
            var apiResult = await _http.AsyncGet(url);
            var result = JsonConvert.DeserializeObject<Season>(apiResult);
            result.Anime = result.Anime.Take(10).ToList();
            return SuccessJsonContent(result);
        }

        [HttpPost]
        public async Task<ContentResult> LoadSeasonalAnime(int year, string season)
        {
            var url = $"{baseUrl}/season/{year}/{season}";
            var apiResult = await _http.AsyncGet(url);
            var result = JsonConvert.DeserializeObject<Season>(apiResult);
            return SuccessJsonContent(result);
        }

        public async Task<ContentResult> GetAnimeStats(int id)
        {
            var url = $"{baseUrl}/anime/{id}/stats";
            var apiResult = await _http.AsyncGet(url);
            var result = JsonConvert.DeserializeObject<AnimeStats>(apiResult);
            return SuccessJsonContent(result);
        }

        public async Task<ContentResult> GetInfoFromDb()
        {
            var query = @"SELECT randominteger AS ID FROM testTable";
            using (_crud.GetOpenConnection())
            {
                var result = await _crud.GetAsync<TestClass>(1);
                return SuccessJsonContent(result);
            }
        }

        public class AnimeStats
        {
            public int watching { get; set; }
            public int completed { get; set; }
        }

        [Table("testTable")]
        public class TestClass
        {
            [Column("randominteger")]
            public int ID { get; set; }
        }
    }
}
