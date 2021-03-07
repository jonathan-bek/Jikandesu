using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Jikandesu.Areas.Home.Models;
using Jikandesu.Models;
using Jikandesu.Services;
using Newtonsoft.Json;

namespace Jikandesu.Areas.Home.Controllers
{
    public class SearchResults
    {
        [JsonProperty("results")] public List<SearchResult> results { get; set; }
    }
    public class SearchResult
    {
        [JsonProperty("mal_id")] public int ID { get; set; }
        [JsonProperty("url")] public string Url { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("image_url")] public string ImageUrl { get; set; }
        [JsonProperty("type")] public string Type { get; set; }
        [JsonProperty("synopsis")] public string Synopsis { get; set; }
        [JsonProperty("members")] public int Members { get; set; }
        [JsonProperty("score")] public decimal? Score { get; set; }
        [JsonProperty("start_date")] public DateTime? StartDate { get; set; }
        [JsonProperty("end_date")] public DateTime? EndDate { get; set; }
        [JsonProperty("episodes")] public int? Episodes { get; set; } //anime only
        [JsonProperty("rated")] public string Rated { get; set; } //anime only
        [JsonProperty("chapters")] public int? Chapters { get; set; } //manga only
        [JsonProperty("volumes")] public int? Volumes { get; set; } //manga only

        [JsonProperty("airing")]
        public bool Airing { get; set; } //anime only
        [JsonProperty("publishing")]
        public bool Publishing { get; set; } //manga only
    }

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

            //Parallel.ForEach(filterCollection, f =>
            //{
            //    var url = $"{baseUrl}/search/{CreateSearchQueryString(f)}";

            //});

            var str = "Placeholder";

            return SuccessJsonContent(str);
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
            var query = @"SELECT * FROM testTable";
            using (_crud.GetOpenConnection())
            {
                var result = await _crud.QueryAsync<TestClass>(query);
                var info = result.Select(x => x.ID).ToList();
                return SuccessJsonContent(info);
            }
        }

        public class AnimeStats
        {
            public int watching { get; set; }
            public int completed { get; set; }
        }

        public class TestClass
        {
            public int ID { get; set; }
        }
    }
}
