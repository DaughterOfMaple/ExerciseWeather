using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExerciseWeather.ViewModels
{
    public interface INavigationService
    {
        Task PushAsync(Page page);
        Task<Page> PopAsync();

        Task DisplayAlert(string title, string message, string ok);
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);

        void SetNavBarBackgroundColor(Color color);
    }
}
