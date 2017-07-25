using Newtonsoft.Json;
using System.Collections.Generic;

namespace ArcTMDb.Models
{
    public class MovieResults
    {
        public int Page { get; set; }

        public List<MovieDetails> Results { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }
    }
}
