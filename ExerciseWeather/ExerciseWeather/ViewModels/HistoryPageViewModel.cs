using ExerciseWeather.Models.Exercise;
using ExerciseWeather.Models.Weather;
using ExerciseWeather.Persistence;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ExerciseWeather.ViewModels
{
    public class HistoryPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly string userId = Constants.user; // Logic for login accounts

        private int _currentStreak;
        private int _longestStreak;
        private int _totalWorkouts;

        public int CurrentStreak
        {
            get => _currentStreak;
            set => SetValue(ref _currentStreak, value);
        }
        public int LongestStreak
        {
            get => _longestStreak;
            set => SetValue(ref _longestStreak, value);
        }
        public int TotalWorkouts
        {
            get => _totalWorkouts;
            set => SetValue(ref _totalWorkouts, value);
        }

        public Color BackgroundColor { get; private set; }
        public Color NavBarBackgroundColor { get; private set; }

        public ICommand LoadDataCommand { get; private set; }
        public ICommand FetchWeatherCommand;

        public HistoryPageViewModel(INavigationService navigationService, WeatherDay weatherToday)
        {
            _navigationService = navigationService;
            UpdateUI(weatherToday);

            // Execute OnAppearing
            LoadDataCommand = new Command(async () => await LoadDataAsync());
            FetchWeatherCommand = new Command(async () => await FetchWeatherAsync());
        }

        private void UpdateUI(WeatherDay day)
        {
            if (day is null)
                day = new WeatherDay();

            BackgroundColor = day.BackgroundColor;
            _navigationService.SetNavBarBackgroundColor(day.BackgroundColor);
        }

        private async Task LoadDataAsync()
        {
            StatsDatabase database = await StatsDatabase.Instance;
            UserStats stats = await database.GetStatsAsync(userId);

            SetCurrentUserStats(stats);
        }

        private async Task FetchWeatherAsync()
        {
            var weatherManager = new WeatherManager(new RestService(), Preferences.Get(Constants.City, string.Empty));
            var weatherData = await weatherManager.FetchWeatherAsync();
            var weatherToday = weatherManager.GetDay(weatherData, Preferences.Get(Constants.IsNight, false));
            UpdateUI(weatherToday);
        }

        private void SetCurrentUserStats(UserStats stats)
        {
            CurrentStreak = stats?.CurrentStreak ?? 0;
            LongestStreak = stats?.LongestStreak ?? 0;
            TotalWorkouts = stats?.TotalWorkouts ?? 0;
        }
    }
}
