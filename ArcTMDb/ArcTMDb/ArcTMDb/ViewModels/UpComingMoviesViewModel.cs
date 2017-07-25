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
    public class UpComingMoviesViewModel : BaseViewModel
    {
        private readonly IArcTMDbApiService _arcTMDbApiService;

        private GenreResults _genreResults;

        private int _page = 0;

        public ObservableCollection<MovieDetails> UpComingMovies { get; }

        public Command<MovieDetails> LoadMoreMoviesCommand { get; }

        public Command<MovieDetails> ShowMovieDetailsCommand { get; }

        public Command ShowSearchMovieCommand { get; }

        public UpComingMoviesViewModel(IArcTMDbApiService arcTMDbApiService)
        {
            _arcTMDbApiService = arcTMDbApiService;

            UpComingMovies = new ObservableCollection<MovieDetails>();

            LoadMoreMoviesCommand = new Command<MovieDetails>(async (movieDetails) => await ExecuteLoadMoreMoviesCommand(), CanExecuteLoadMoreMoviesCommand);

            LoadMoreMoviesCommand.Execute(new MovieDetails());

            ShowMovieDetailsCommand = new Command<MovieDetails>(async (movieDetails) => await ExecuteShowMovieDetailsCommand(movieDetails));

            ShowSearchMovieCommand = new Command(async() => await ExecuteShowSearchMovieCommand());
        }

        private async Task ExecuteLoadMoreMoviesCommand()
        {
            try
            {
                if (_genreResults == null)
                    _genreResults = await _arcTMDbApiService.GetGenresAsync();

                var upComingMovies = await _arcTMDbApiService.GetUpcomingMoviesAsync(++_page);

                if (upComingMovies != null)
                {
                    foreach (var movieDetails in upComingMovies.Results)
                    {
                        movieDetails.GenreNames = GenreUtility.GetGenreNames(movieDetails.GenreIds, _genreResults);
                        UpComingMovies.Add(movieDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private bool CanExecuteLoadMoreMoviesCommand(MovieDetails movieDetails)
        {
            return movieDetails == UpComingMovies[UpComingMovies.Count - 1];
        }

        private async Task ExecuteShowSearchMovieCommand()
        {
            try
            {
                await PushAsync<SearchMoviesViewModel>(_arcTMDbApiService);
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
