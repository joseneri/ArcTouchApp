using ArcTMDb.Models;
using ArcTMDb.Service;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace ArcTMDb.ViewModels
{
    public class SearchMoviesViewModel : BaseViewModel
    {
        private readonly IArcTMDbApiService _arcTMDbApiService;
       
        private GenreResults _genresResults;

        private int page = 0;

        private string _searchTerm;
        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                if (SetProperty(ref _searchTerm, value))
                    SearchCommand.ChangeCanExecute();
            }
        }

        public ObservableCollection<MovieDetails> SearchMovies { get; set; }

        public Command SearchCommand { get; }

        public Command<MovieDetails> LoadMoreMoviesCommand { get; set; }

        public Command<MovieDetails> ShowMovieDetailsCommand { get; }

        public SearchMoviesViewModel(IArcTMDbApiService arcTMDbApiService)
        {
            _arcTMDbApiService = arcTMDbApiService;

            GetGenres();

            SearchMovies = new ObservableCollection<MovieDetails>();

            SearchCommand = new Command(ExecuteSearch, CanExecuteSearch);

            LoadMoreMoviesCommand = new Command<MovieDetails>(ExecuteLoadMoreMovies, CanExecuteLoadMoreMovies);

            ShowMovieDetailsCommand = new Command<MovieDetails>(ExecuteShowMovieDetails);
        }

        void ExecuteSearch()
        {
            page = 0;

            SearchMovies.Clear();

            LoadSearchMovies();
        }

        bool CanExecuteSearch()
        {
            return string.IsNullOrWhiteSpace(SearchTerm) == false;
        }

        private void ExecuteLoadMoreMovies(MovieDetails movieDetails)
        {
            LoadSearchMovies();
        }

        private async void LoadSearchMovies()
        {
            var moviesByTitle = await _arcTMDbApiService.GetMoviesByTitleAsync(SearchTerm, ++page);

            if (moviesByTitle != null)
            {
                foreach (var movieDetails in moviesByTitle.Results)
                {
                    movieDetails.GenreNames = GetGenreNames(movieDetails.Genre_ids);
                    SearchMovies.Add(movieDetails);
                }
            }
        }

        private bool CanExecuteLoadMoreMovies(MovieDetails movieDetails)
        {
            if (movieDetails == SearchMovies[SearchMovies.Count - 1])
                return true;

            return false;
        }

        private async void ExecuteShowMovieDetails(MovieDetails movieDetails)
        {
            await PushAsync<MovieDetailsViewModel>(_arcTMDbApiService, movieDetails);
        }

        private async void GetGenres()
        {
            _genresResults = await _arcTMDbApiService.GetGenres();
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
