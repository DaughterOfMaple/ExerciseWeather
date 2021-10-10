using Xamarin.Forms;

namespace ExerciseWeather.Models.Weather
{
    public class SunnyDay : WeatherDay
    {
        public SunnyDay()
        {
            Message = "The sun is shining, time to go outside!";
            BackgroundColor = Color.FromHex("#FFEF78");
        }
    }
}
