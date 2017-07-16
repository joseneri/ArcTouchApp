using System.Collections.Generic;

namespace ArcTMDb.Models
{
    public class MovieResults
    {
        public int Page { get; set; }
        public List<MovieDetails> Results { get; set; }
        public int Total_pages { get; set; }
        public int Total_results { get; set; }
    }
}
