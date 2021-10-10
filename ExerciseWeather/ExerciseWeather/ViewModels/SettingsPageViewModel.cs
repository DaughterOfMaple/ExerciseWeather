using ExerciseWeather.Models.Weather;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ExerciseWeather.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        private Color _backgroundColor;

        public Color BackgroundColor
        {
            get => _backgroundColor;
            set => SetValue(ref _backgroundColor, value);
        }
        public string CityName 
        {
            get => Preferences.Get(Constants.City, "");
            set
            {
                Preferences.Set(Constants.City, value);
                OnPropertyChanged();
            }
        }
        public int ExerciseTime
        {
            get => Preferences.Get(Constants.ExerciseTime, 1);
            set
            {
                Preferences.Set(Constants.ExerciseTime, value);
                OnPropertyChanged();
            }
        }
        public int RestTime
        {
            get => Preferences.Get(Constants.RestTime, 15);
            set
            {
                Preferences.Set(Constants.RestTime, value);
                OnPropertyChanged();
            }
        }
        public int NumberOfExercises
        {
            get => Preferences.Get(Constants.NumberofExercises, 5);
            set
            {
                Preferences.Set(Constants.NumberofExercises, value);
                OnPropertyChanged();
            }
        }
        public bool IsNight // Refresh background and weather on toggle
        {
            get => Preferences.Get(Constants.IsNight, false);
            set
            {
                Preferences.Set(Constants.IsNight, value);
                OnPropertyChanged();
                _ = FetchWeatherAsync();
            }
        }

        public ICommand FetchWeatherCommand { get; private set; }

        public SettingsPageViewModel(INavigationService navigationService, WeatherDay weatherToday)
        {
            _navigationService = navigationService;
            UpdateUI(weatherToday);

            // Execute OnAppearing
            FetchWeatherCommand = new Command(async () => await FetchWeatherAsync());
        }

        private void UpdateUI(WeatherDay day)
        {
            if (day is null)
                day = new WeatherDay();

            BackgroundColor = day.BackgroundColor;
            _navigationService.SetNavBarBackgroundColor(day.BackgroundColor);
        }

        private async Task FetchWeatherAsync()
        {
            var weatherManager = new WeatherManager(new RestService(), Preferences.Get(Constants.City, string.Empty));
            var weatherData = await weatherManager.FetchWeatherAsync();
            var weatherToday = weatherManager.GetDay(weatherData, Preferences.Get(Constants.IsNight, false));
            UpdateUI(weatherToday);
        }
    }
}
