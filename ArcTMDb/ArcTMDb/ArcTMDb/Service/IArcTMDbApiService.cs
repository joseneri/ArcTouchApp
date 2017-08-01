using ArcTMDb.Models;
using System.Threading.Tasks;

namespace ArcTMDb.Service
{
    public interface IArcTMDbApiService
    {
        Task<MovieResults> GetUpcomingMoviesAsync(int page = 1);

        Task<MovieResults> GetMoviesByTitleAsync(string title, int page = 1);

        Task<GenreResults> GetGenresAsync();
    }
}
