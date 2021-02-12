using System.Threading.Tasks;
using System.Web.Mvc;
using Jikandesu.Areas.Home.Models;
using Jikandesu.Models;
using Jikandesu.Services;

namespace Jikandesu.Areas.Home.Controllers
{
    public class HomeApiController : BaseApiController
    {
        private readonly IJdHttpService _http;

        private const string baseUrl = "https://api.jikan.moe/v3";

        public HomeApiController(IJdHttpService http)
        {
            _http = http;
        }

        // GET: Home/HomeApi
        public async Task<ContentResult> GetAnimeStats(int id)
        {
            var lst = new DbTester().Run();
            var result = lst[0];
            //var url = $"{baseUrl}/anime/{id}/stats";
            //var getResult = await _http.AsyncGet(url);
            //var result = JsonConvert.DeserializeObject<AnimeStats>(getResult);
            return SuccessJsonContent(result);
        }

        public class AnimeStats
        {
            public int watching { get; set; }
            public int completed { get; set; }
        }
    }
}
