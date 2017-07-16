using ArcTMDb.Service;
using ArcTMDb.ViewModels;
using Xamarin.Forms;

namespace ArcTMDb.Views
{
    public partial class UpComingMoviesPage : ContentPage
    {
        public UpComingMoviesPage()
        {
            InitializeComponent();

            BindingContext = new UpComingMoviesViewModel(new ArcTMDbApiService());
        }
    }
}
