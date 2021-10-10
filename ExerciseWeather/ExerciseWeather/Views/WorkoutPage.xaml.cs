using ExerciseWeather.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExerciseWeather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutPage : ContentPage
    {
        public WorkoutPage()
        {
            ViewModel = new WorkoutPageViewModel(new PageService(), new NavigationService());
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            ViewModel.FetchWeatherCommand.Execute(null);
            base.OnAppearing();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            ViewModel.ResizeScreenCommand.Execute(null);
            base.OnSizeAllocated(width, height);
        }

        public WorkoutPageViewModel ViewModel
        {
            get => BindingContext as WorkoutPageViewModel;
            set => BindingContext = value;
        }
    }
}
