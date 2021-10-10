using Xamarin.Forms;

namespace ExerciseWeather.Models.Weather
{
    public class WeatherDay
    {
        public string Message { get; set; } = "What's the weather like today? " +
            "Check your city in Settings or your internet connection.";
        public Color BackgroundColor { get; set; } = Color.LightGreen;
        public Color ButtonBackgroundColor { get; set; } = Color.Snow;
        public Color ButtonTextColour { get; set; } = Color.Black;
        public Color LabelTextColor { get; set; } = Color.Black;
    }
}
