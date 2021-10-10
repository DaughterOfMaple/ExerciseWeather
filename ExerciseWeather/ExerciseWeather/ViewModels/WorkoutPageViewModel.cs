using ExerciseWeather.Models.Exercise;
using ExerciseWeather.Models.Weather;
using ExerciseWeather.Persistence;
using ExerciseWeather.Views;
using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ExerciseWeather.ViewModels
{
    public class WorkoutPageViewModel : BaseViewModel
    {
        // Private fields
        private readonly IPageService _pageService;
        private readonly INavigationService _navigationService;

        private bool _isPortraitMode;

        private Color _backgroundColor, _buttonBackgroundColor, 
            _buttonTextColor, _labelTextColor;

        private string _startBtnText;
        private Color _startBtnColor;

        private WeatherDay weatherToday;
        private string _weatherMessage;

        private ExerciseList exercises;
        private int currentExerciseIndex;
        private string _currentExercise;
        private int _numberOfExercisesRemaining;

        private readonly Timer timer;
        private int _timeRemaining, _minutesRemaining, _secondsRemaining;

        private readonly string userId = Constants.user; //TODO: Add login logic
        private bool isExistingUser;
        private UserStats stats;
        private int _currentStreak, _longestStreak, _totalWorkouts;
        private string _lastWorkout;

        // Properties for Binding
        public bool IsPortraitMode
        {
            get => _isPortraitMode;
            set => SetValue(ref _isPortraitMode, value);
        }

        public Color BackgroundColor
        {
            get => _backgroundColor;
            set => SetValue(ref _backgroundColor, value);
        }
        public Color ButtonBackgroundColor
        {
            get => _buttonBackgroundColor;
            set => SetValue(ref _buttonBackgroundColor, value);
        }
        public Color ButtonTextColor
        {
            get => _buttonTextColor;
            set => SetValue(ref _buttonTextColor, value);
        }
        public Color LabelTextColor
        {
            get => _labelTextColor;
            set => SetValue(ref _labelTextColor, value);
        }

        public string WeatherMessage
        {
            get => _weatherMessage;
            set => SetValue(ref _weatherMessage, value);
        }

        public string CurrentExercise
        {
            get => _currentExercise;
            set => SetValue(ref _currentExercise, value);
        }
        public int ExercisesRemaining
        {
            get => _numberOfExercisesRemaining;
            set => SetValue(ref _numberOfExercisesRemaining, value);
        }
        public int TimeRemaining // Set values of MinutesRemaining and SecondsRemaining
        {
            get => _timeRemaining;
            set
            {
                SetValue(ref _timeRemaining, value);
                MinutesRemaining = TimeRemaining / 60;
                SecondsRemaining = TimeRemaining % 60;
            }
        }
        public int MinutesRemaining
        {
            get => _minutesRemaining;
            set => SetValue(ref _minutesRemaining, value);
        }
        public int SecondsRemaining
        {
            get => _secondsRemaining;
            set => SetValue(ref _secondsRemaining, value);
        }

        public Color StartBtnColor
        {
            get => _startBtnColor;
            set => SetValue(ref _startBtnColor, value);
        }
        public string StartBtnText
        {
            get => _startBtnText;
            set => SetValue(ref _startBtnText, value);
        }

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
        public string LastWorkout
        {
            get => _lastWorkout;
            set => SetValue(ref _lastWorkout, value);
        }

        // Commands
        public ICommand LoadDataCommand { get; private set; }
        public ICommand FetchWeatherCommand { get; private set; }
        public ICommand ResizeScreenCommand { get; private set; }
        public ICommand ToHistoryPageCommand { get; private set; }
        public ICommand ToSettingsPageCommand { get; private set; }
        public ICommand StartBtnCommand { get; private set; }
        public ICommand ResetBtnCommand { get; private set; }
        public ICommand SkipBtnCommand { get; private set; }
        public ICommand ReverseBtnCommand { get; private set; }

        public WorkoutPageViewModel(IPageService pageService, INavigationService navigationService)
        {
            _pageService = pageService;
            _navigationService = navigationService;

            // Execute OnAppearing
            LoadDataCommand = new Command(async () => await LoadDataAsync());
            FetchWeatherCommand = new Command(async () => await FetchWeatherAsync());

            // Execute OnSizeAllocated
            ResizeScreenCommand = new Command(ResizeScreen);

            // Navigation commands
            ToHistoryPageCommand = new Command(async () => await ToHistoryPageAsync());
            ToSettingsPageCommand = new Command(async () => await ToSettingsPageAsync());

            // Button commands
            StartBtnCommand = new Command(ToggleWorkout);
            ResetBtnCommand = new Command(ResetWorkout);
            SkipBtnCommand = new Command(SkipExercise);
            ReverseBtnCommand = new Command(ReverseExercise);

            timer = new Timer(1000);
            timer.Elapsed += async (sender, e) => await OnTimedEventAsync();

            SetCurrentExerciseValues();
            ToggleStartBtnStyle();
            UpdateUI(new WeatherDay());
        }

        private async Task LoadDataAsync()
        {
            var database = await StatsDatabase.Instance;
            stats = await database.GetStatsAsync(userId);

            if (!(stats is null))
                isExistingUser = true;

            SetCurrentUserStats(stats);
        }

        private void SetCurrentUserStats(UserStats stats)
        {
            CurrentStreak = stats?.CurrentStreak ?? 0;
            LongestStreak = stats?.LongestStreak ?? 0;
            TotalWorkouts = stats?.TotalWorkouts ?? 0;
            LastWorkout = stats?.LastWorkout ?? DateTime.MinValue.ToString();
        }

        private async Task FetchWeatherAsync()
        {
            var weatherManager = new WeatherManager(new RestService(), Preferences.Get(Constants.City, string.Empty));
            var weatherData = await weatherManager.FetchWeatherAsync();
            weatherToday = weatherManager.GetDay(weatherData, Preferences.Get(Constants.IsNight, false));
            UpdateUI(weatherToday);
        }

        private void UpdateUI(WeatherDay day)
        {
            BackgroundColor = day.BackgroundColor;
            ButtonBackgroundColor = day.ButtonBackgroundColor;
            ButtonTextColor = day.ButtonTextColour;
            LabelTextColor = day.LabelTextColor;
            WeatherMessage = day.Message;
        }

        private void ResizeScreen()
        {
            double width = 0;
            double height = 0;
            if (width != _pageService.GetWidth() || height != _pageService.GetHeight())
            {
                width = _pageService.GetWidth();
                height = _pageService.GetHeight();
                IsPortraitMode = height > width;
            }
        }

        private async Task ToHistoryPageAsync()
        {
            await _navigationService.PushAsync(new HistoryPage(weatherToday));
        }

        private async Task ToSettingsPageAsync()
        {
            await _navigationService.PushAsync(new SettingsPage(weatherToday));
        }

        private void ToggleWorkout()
        {
            // Stop exercise
            if (timer.Enabled)
            {
                timer.Stop();
                ToggleStartBtnStyle();
            }
            // Continue exercise
            else if (TimeRemaining > 0)
            {
                timer.Start();
                ToggleStartBtnStyle();
            }
            // Start new workout
            else
            {
                currentExerciseIndex = 0;
                exercises = new ExerciseList(Preferences.Get(Constants.NumberofExercises, 5));
                SetCurrentExerciseValues();

                timer.Start();
                ToggleStartBtnStyle();
            }
        }

        private void ToggleStartBtnStyle()
        {
            if (timer is null || !timer.Enabled)
            {
                StartBtnText = Constants.Start;
                StartBtnColor = Constants.StartBtnGreen;
            }
            else
            {
                StartBtnText = Constants.Pause;
                StartBtnColor = Constants.StartBtnRed;
            }
        }

        private void SetCurrentExerciseValues()
        {
            var exerciseTime = Preferences.Get(Constants.ExerciseTime, 1) * 60;
            var restTime = Preferences.Get(Constants.RestTime, 15);

            if (exercises is null || exercises.List.Count == 0)
            {
                CurrentExercise = "Ready?";
                TimeRemaining = 0;
                ExercisesRemaining = 0;
            }
            else if (currentExerciseIndex >= 0 && currentExerciseIndex < exercises.List.Count)
            {
                CurrentExercise = exercises.List[currentExerciseIndex];
                TimeRemaining = Equals(CurrentExercise, Constants.Rest) ? restTime : exerciseTime;
                ExercisesRemaining = (exercises.List.Count - currentExerciseIndex - 1) / 2;
            }
        }

        private void ResetWorkout()
        {
            timer.Stop();
            ToggleStartBtnStyle();

            CurrentExercise = "Ready?";
            TimeRemaining = 0;
            currentExerciseIndex = int.MaxValue;
            exercises = new ExerciseList(Preferences.Get(Constants.NumberofExercises, 5));
            ExercisesRemaining = 0;
        }

        private void SkipExercise()
        {
            if (!(exercises is null) && currentExerciseIndex < exercises.List.Count - 1)
                currentExerciseIndex += 1;
            
            SetCurrentExerciseValues();
        }

        private void ReverseExercise()
        {
            if (currentExerciseIndex > 0)
                currentExerciseIndex -= 1;

            SetCurrentExerciseValues();
        }

        private async Task OnTimedEventAsync()
        {
            // Count down to zero
            if (TimeRemaining > 0)
            {
                TimeRemaining -= 1;
            }
            // Next exercise
            else if (currentExerciseIndex < exercises.List.Count - 1)
            {
                currentExerciseIndex += 1;
                SetCurrentExerciseValues();
            }
            // Workout completed
            else
            {
                timer.Stop();
                ToggleStartBtnStyle();
                CurrentExercise = "You did it!";
                currentExerciseIndex = int.MaxValue;
                UpdateUserStats();
                await SaveDataAsync();
            }
        }

        private void UpdateUserStats()
        {
            // Update currentStreak value
            DateTime lastWorkout = Convert.ToDateTime(LastWorkout);
            DateTime today = DateTime.Today;
            if (today - lastWorkout == TimeSpan.FromDays(1))
                CurrentStreak += 1;
            else if (today - lastWorkout > TimeSpan.FromDays(1))
                CurrentStreak = 1;

            // Update longestStreak value
            if (CurrentStreak > LongestStreak)
                LongestStreak = CurrentStreak;

            // Update totalWorkouts value
            TotalWorkouts += 1;

            // Update lastWorkout value
            LastWorkout = today.ToString();
        }

        private async Task SaveDataAsync()
        {
            var stats = new UserStats()
            {
                ID = userId,
                CurrentStreak = CurrentStreak,
                LongestStreak = LongestStreak,
                TotalWorkouts = TotalWorkouts,
                LastWorkout = LastWorkout
            };
            var database = await StatsDatabase.Instance;

            if (isExistingUser)
            {
                await database.UpdateStatsAsync(stats);
            }
            else
            {
                await database.AddStatsAsync(stats);
                isExistingUser = true;
            }
        }
    }
}
