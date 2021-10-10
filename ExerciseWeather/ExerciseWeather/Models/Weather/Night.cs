using Xamarin.Forms;

namespace ExerciseWeather.Models.Weather
{
    public class Night : WeatherDay
    {
        public Night()
        {
            Message = "Hey there, night owl. Keep it up!";
            BackgroundColor = Color.FromHex("#010310");
            ButtonBackgroundColor = Color.FromHex("#80fffafa");
            ButtonTextColour = Color.FromHex("#010310");
            LabelTextColor = Color.FromHex("#80fffafa");
        }
    }
}
