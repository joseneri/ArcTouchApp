using ArcTMDb.Helper;

namespace ArcTMDb.Models
{
    public class MovieDetails
    {
        public string Poster_path { get; set; }
        public bool Adult { get; set; }
        public string Overview { get; set; }
        public string Release_date { get; set; }
        public int[] Genre_ids { get; set; }
        public int Id { get; set; }
        public string Original_title { get; set; }
        public string Original_language { get; set; }
        public string Title { get; set; }
        public string Backdrop_path { get; set; }
        public float Popularity { get; set; }
        public int Vote_count { get; set; }
        public bool Video { get; set; }
        public float Vote_average { get; set; }
        public string GenreNames { get; set; }
        public string Details => string.Format("{0}  {1}", Release_date, GenreNames);
        public string FullImagePath => string.Format("{0}{1}", Constants.ImageBaseURLW342, Poster_path);
        public string FullSmallImagePath => string.Format("{0}{1}", Constants.ImageBaseURLW154, Poster_path);
    }
}
