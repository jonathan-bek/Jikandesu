using System.Threading.Tasks;
using System.Web.Mvc;
using Jikandesu.Models;
using Jikandesu.Services;
using Newtonsoft.Json;

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
            var url = $"{baseUrl}/anime/{id}/stats";
            var getResult = await _http.AsyncGet(url);
            var result = JsonConvert.DeserializeObject<AnimeStats>(getResult);
            return SuccessJsonContent(result);
        }

        public class AnimeStats
        {
            [JsonProperty("watching")]
            public int ID { get; set; }
            [JsonProperty("completed")]
            public int Title { get; set; }
        }
    }
}
