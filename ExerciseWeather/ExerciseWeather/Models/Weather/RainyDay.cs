using Xamarin.Forms;

namespace ExerciseWeather.Models.Weather
{
    public class RainyDay : WeatherDay
    {
        public RainyDay()
        {
            Message = "Looks like rain! Today can be an inside day :)";
            BackgroundColor = Color.FromHex("#142850");
        }
    }
}
