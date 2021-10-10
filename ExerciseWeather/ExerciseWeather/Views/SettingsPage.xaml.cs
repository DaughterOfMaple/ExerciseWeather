using ExerciseWeather.Models.Weather;
using ExerciseWeather.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExerciseWeather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage(WeatherDay weatherToday)
        {
            InitializeComponent();
            ViewModel = new SettingsPageViewModel(new NavigationService(), weatherToday);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.FetchWeatherCommand.Execute(null);
        }

        public SettingsPageViewModel ViewModel
        {
            get => BindingContext as SettingsPageViewModel;
            set => BindingContext = value;
        }
    }
}
