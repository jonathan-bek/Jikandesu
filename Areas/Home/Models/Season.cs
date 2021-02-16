using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Jikandesu.Areas.Home.Models
{
    public class Season
    {
        [JsonProperty("season_name")]
        public string Name { get; set; }
        [JsonProperty("season_year")]
        public int Year { get; set; }
        [JsonProperty("anime")]
        public List<Anime> Anime { get; set; }
    }
}