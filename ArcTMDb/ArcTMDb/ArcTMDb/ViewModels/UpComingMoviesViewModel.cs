using ArcTMDb.Models;
using ArcTMDb.Service;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace ArcTMDb.ViewModels
{
    public class UpComingMoviesViewModel : BaseViewModel
    {
        private readonly IArcTMDbApiService _arcTMDbApiService;

        private GenreResults _genresResults;

        private int page = 0;

        public ObservableCollection<MovieDetails> UpComingMovies { get; set; }

        public Command<MovieDetails> LoadMoreMoviesCommand { get; set; }

        public Command<MovieDetails> ShowMovieDetailsCommand { get; }

        public Command ShowSearchMovieCommand { get; }

        public UpComingMoviesViewModel(IArcTMDbApiService arcTMDbApiService)
        {
            _arcTMDbApiService = arcTMDbApiService;

            UpComingMovies = new ObservableCollection<MovieDetails>();

            LoadUpCommingMovies();

            LoadMoreMoviesCommand = new Command<MovieDetails>(ExecuteLoadMoreMovies, CanExecuteLoadMoreMovies);

            ShowMovieDetailsCommand = new Command<MovieDetails>(ExecuteShowMovieDetails);

            ShowSearchMovieCommand = new Command(ExecuteShowSearchMovie);
        }

        private void ExecuteLoadMoreMovies(MovieDetails movieDetails)
        {
            LoadUpCommingMovies();
        }

        private bool CanExecuteLoadMoreMovies(MovieDetails movieDetails)
        {
            if (movieDetails == UpComingMovies[UpComingMovies.Count - 1])
                return true;

            return false;
        }

        private async void ExecuteShowSearchMovie()
        {
            await PushAsync<SearchMoviesViewModel>(_arcTMDbApiService);
        }

        private async void ExecuteShowMovieDetails(MovieDetails movieDetails)
        {
            await PushAsync<MovieDetailsViewModel>(_arcTMDbApiService, movieDetails);
        }

        private async void LoadUpCommingMovies()
        {
            if (_genresResults == null)
                _genresResults = await _arcTMDbApiService.GetGenres();

            var upComingMovies = await _arcTMDbApiService.GetUpcomingMoviesAsync(++page);

            if (upComingMovies != null)
            {
                foreach (var movieDetails in upComingMovies.Results)
                {
                    movieDetails.GenreNames = GetGenreNames(movieDetails.Genre_ids);
                    UpComingMovies.Add(movieDetails);
                }
            }
        }

        private string GetGenreNames(int[] genresId)
        {
            string genreNames = string.Empty;
            string split = " | ";

            if (genresId.Count() == 0 || _genresResults == null)
                return genreNames;

            foreach (var genre in _genresResults.Genres)
            {
                if (genresId.Contains(genre.Id))
                    genreNames += genre.Name + split;
            }

            return genreNames.Remove(genreNames.Length - split.Length);
        }
    }
}
