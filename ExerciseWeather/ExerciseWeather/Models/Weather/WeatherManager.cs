using ExerciseWeather.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ExerciseWeather.Models.Weather
{
    public class WeatherManager
    {
        private readonly IRestService restService;
        private readonly string city;

        public WeatherManager(IRestService restService, string city)
        {
            this.restService = restService;
            this.city = city;
        }

        public async Task<CurrentWeather> FetchWeatherAsync()
        {
            string APIString = $"{Constants.APIWeatherString}{city}";
            var weather = new CurrentWeather();

            try
            {
                weather = await restService.FetchWeatherData(APIString);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"\t\tERROR {e.Message}"); // TODO: Add alert message
            }
            return weather;
        }

        public WeatherDay GetDay(CurrentWeather weather, bool isNight)
        {
            if (isNight)
                return new Night();

            if (weather?.Weather?.Number is null)
                return new WeatherDay();

            switch (weather.Weather.Number)
            {
                case int n when n > 800:
                    return new CloudyDay();
                case 800:
                    return new SunnyDay();
                case int n when n > 700:
                    return new CloudyDay();
                case int n when n >= 200:
                    return new RainyDay();
                default:
                    return new WeatherDay();
            }
        }
    }
}
