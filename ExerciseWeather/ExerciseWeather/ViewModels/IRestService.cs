using ExerciseWeather.Models.Weather;
using System.Threading.Tasks;

namespace ExerciseWeather.ViewModels
{
    public interface IRestService
    {
        Task<CurrentWeather> FetchWeatherData(string query);
    }
}