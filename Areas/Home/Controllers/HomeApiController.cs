using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        private const string baseUrl = "https://api.jikan.moe/v3";

        public HomeApiController(IJdCrud crud,
            IJdHttpService http)
        {
            _crud = crud;
            _http = http;
        }

        [HttpPost]
        public async Task<ContentResult> LoadSearchResults(
            IEnumerable<SearchFilter> filterCollection)
        {
            var animeFilter = filterCollection.First(x => x.SearchCategory.Equals(SearchCategoryEnum.Anime));
            var animeResult = await _http.AsyncGet($"{baseUrl}/search/{CreateSearchQueryString(animeFilter)}&page=1");
            var mangaFilter = filterCollection.First(x => x.SearchCategory.Equals(SearchCategoryEnum.Manga));
            var mangaResult = await _http.AsyncGet($"{baseUrl}/search/{CreateSearchQueryString(mangaFilter)}&page=1");

            var result1 = JsonConvert.DeserializeObject<SearchResults>(animeResult);
            var result2 = JsonConvert.DeserializeObject<SearchResults>(mangaResult);

            var str = "Placeholder";

            return SuccessJsonContent(new { result1, result2 });
        }

        private string CreateSearchQueryString(SearchFilter filter)
        {
            return $"{filter.SearchCategory}?q={filter.Name}";
        }

        [HttpGet]
        public async Task<ContentResult> LoadScrapedMangaImageUrls()
        {
            var str = "Placeholder";

            return SuccessJsonContent(str);
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
