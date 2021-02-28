using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Dapper;
using Jikandesu.Areas.Home.Models;
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
            using (var con = _crud.GetOpenConnection())
            {
                var result = await con.QueryAsync<TestClass>(query);
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
