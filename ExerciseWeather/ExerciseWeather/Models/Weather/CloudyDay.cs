using Xamarin.Forms;

namespace ExerciseWeather.Models.Weather
{
    public class CloudyDay : WeatherDay
    {
        public CloudyDay()
        {
            Message = "The clouds are in. Are you feeling lucky?";
            BackgroundColor = Color.FromHex("#72858b");
        }
    }
}
