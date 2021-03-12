using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Jikandesu.Areas.Home.Models.JsonModels
{
    public class SearchResults
    {
        [JsonProperty("results")] public List<SearchResult> Results { get; set; }
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
        [JsonProperty("airing")] public bool Airing { get; set; } //anime only
        [JsonProperty("episodes")] public int? Episodes { get; set; } //anime only
        [JsonProperty("rated")] public string Rated { get; set; } //anime only
        [JsonProperty("publishing")] public bool Publishing { get; set; } //manga only
        [JsonProperty("chapters")] public int? Chapters { get; set; } //manga only
        [JsonProperty("volumes")] public int? Volumes { get; set; } //manga only
    }
}
