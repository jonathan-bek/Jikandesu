using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Jikandesu.Areas.Home.Models
{
    public class Anime
    {
        [JsonProperty("mal_id")]
        public int ID { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty("synopsis")]
        public string Synopsis { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("airing_start")]
        public DateTime? AiringStart { get; set; }
        [JsonProperty("episodes")]
        public int? Episodes { get; set; }
        [JsonProperty("members")]
        public int Members { get; set; }
        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("score")]
        public decimal? Score { get; set; }
        [JsonProperty("continuing")]
        public bool Continuing { get; set; }
    }
}
