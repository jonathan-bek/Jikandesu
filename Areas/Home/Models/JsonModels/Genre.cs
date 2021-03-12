using Newtonsoft.Json;

namespace Jikandesu.Areas.Home.Models
{
    public class Genre
    {
        [JsonProperty("mal_id")] public int ID { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("url")] public string Url { get; set; }
        [JsonProperty("type")] public string Type { get; set; }
    }
}
