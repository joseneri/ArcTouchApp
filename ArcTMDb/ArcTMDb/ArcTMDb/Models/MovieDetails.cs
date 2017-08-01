using ArcTMDb.Helpers;
using Newtonsoft.Json;

namespace ArcTMDb.Models
{
    public class MovieDetails
    {
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        public string Overview { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("genre_ids")]
        public int[] GenreIds { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }
        
        public string GenreNames { get; set; }

        public string Details => string.Format("{0}  {1}", ReleaseDate, GenreNames);

        public string FullImagePath => string.Format("{0}{1}", Constants.ImageBaseURLW342, PosterPath);

        public string FullSmallImagePath => string.Format("{0}{1}", Constants.ImageBaseURLW154, PosterPath);
    }
}
