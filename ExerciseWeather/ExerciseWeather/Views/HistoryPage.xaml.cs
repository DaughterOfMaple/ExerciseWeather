using ExerciseWeather.Models.Weather;
using ExerciseWeather.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExerciseWeather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage(WeatherDay weatherToday)
        {
            InitializeComponent();
            ViewModel = new HistoryPageViewModel(new NavigationService(), weatherToday);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadDataCommand.Execute(null);
            ViewModel.FetchWeatherCommand.Execute(null);
        }

        public HistoryPageViewModel ViewModel
        {
            get => BindingContext as HistoryPageViewModel;
            set => BindingContext = value;
        }
    }
}
