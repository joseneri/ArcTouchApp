using ArcTMDb.Models;
using System.Linq;

namespace ArcTMDb.Helpers
{
    public static class GenreUtility
    {
        public static string GetGenreNames(int[] genresId, GenreResults genreResults)
        {
            string genreNames = string.Empty;
            string split = " | ";

            if (genresId.Count() == 0 || genreResults == null)
                return genreNames;

            foreach (var genre in genreResults.Genres)
            {
                if (genresId.Contains(genre.Id))
                    genreNames += genre.Name + split;
            }

            return genreNames.Remove(genreNames.Length - split.Length);
        }
    }
}
