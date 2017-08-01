using ArcTMDb.Models;
using ArcTMDb.Service;

namespace ArcTMDb.ViewModels
{
    public class MovieDetailsViewModel : BaseViewModel
    {
        private readonly IArcTMDbApiService _arcTMDbApiService;

        public MovieDetails MovieDetails { get; set; }

        public MovieDetailsViewModel(IArcTMDbApiService arcTMDbApiService, MovieDetails movieDetails)
        {
            _arcTMDbApiService = arcTMDbApiService; 
            MovieDetails = movieDetails;
        }
    }
}
