using ArcTMDb.Helpers;
using ArcTMDb.Models;
using ArcTMDb.Service;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArcTMDb.ViewModels
{
    public class SearchMoviesViewModel : BaseViewModel
    {
        private readonly IArcTMDbApiService _arcTMDbApiService;
       
        private GenreResults _genreResults;

        private int _page = 0;

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

        public ObservableCollection<MovieDetails> SearchMovies { get; } 

        public Command SearchCommand { get; }

        public Command<MovieDetails> LoadMoreMoviesCommand { get; }

        public Command<MovieDetails> ShowMovieDetailsCommand { get; }

        public SearchMoviesViewModel(IArcTMDbApiService arcTMDbApiService)
        {
            _arcTMDbApiService = arcTMDbApiService;

            SearchMovies = new ObservableCollection<MovieDetails>();
            
            SearchCommand = new Command(async () => await ExecuteSearchCommand(),  CanExecuteSearchCommand);

            LoadMoreMoviesCommand = new Command<MovieDetails>(async (movieDetails) => await ExecuteLoadMoreMoviesCommand(movieDetails), CanExecuteLoadMoreMoviesCommand);

            ShowMovieDetailsCommand = new Command<MovieDetails>(async (movieDetails) => await  ExecuteShowMovieDetailsCommand(movieDetails));
        }

        private async Task ExecuteSearchCommand()
        {
            try
            {
                _page = 0;
                SearchMovies.Clear();

                await LoadSearchMoviesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private bool CanExecuteSearchCommand()
        {
            return string.IsNullOrWhiteSpace(SearchTerm) == false;
        }

        private async Task ExecuteLoadMoreMoviesCommand(MovieDetails movieDetails)
        {
            try
            {
                await LoadSearchMoviesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private bool CanExecuteLoadMoreMoviesCommand(MovieDetails movieDetails)
        {
            return movieDetails == SearchMovies[SearchMovies.Count - 1];
        }

        private async Task LoadSearchMoviesAsync()
        {
            try
            {
                if (_genreResults == null)
                    _genreResults = await _arcTMDbApiService.GetGenresAsync();

                var moviesByTitle = await _arcTMDbApiService.GetMoviesByTitleAsync(SearchTerm, ++_page);

                if (moviesByTitle != null)
                {
                    foreach (var movieDetails in moviesByTitle.Results)
                    {
                        movieDetails.GenreNames = GenreUtility.GetGenreNames(movieDetails.GenreIds, _genreResults);
                        SearchMovies.Add(movieDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task ExecuteShowMovieDetailsCommand(MovieDetails movieDetails)
        {
            try
            {
                await PushAsync<MovieDetailsViewModel>(_arcTMDbApiService, movieDetails);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
