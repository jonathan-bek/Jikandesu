using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Dapper;
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

        // GET: Home/HomeApi
        public async Task<ContentResult> GetAnimeStats(int id)
        {
            var url = $"{baseUrl}/anime/{id}/stats";
            var getResult = await _http.AsyncGet(url);
            var result = JsonConvert.DeserializeObject<AnimeStats>(getResult);
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
