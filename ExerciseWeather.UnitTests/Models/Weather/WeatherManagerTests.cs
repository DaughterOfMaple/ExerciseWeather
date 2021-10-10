using ExerciseWeather.Models.Weather;
using ExerciseWeather.ViewModels;
using NUnit.Framework;
using System;

namespace ExerciseWeather.UnitTests.Models.Weather
{
    [TestFixture]
    public class WeatherManagerTests
    {
        private WeatherManager weatherManager;
        [SetUp]
        public void SetUp()
        {
            weatherManager = new WeatherManager(new RestService(), "");
        }

        [Test]
        [TestCase(801, typeof(CloudyDay))]
        [TestCase(800, typeof(SunnyDay))]
        [TestCase(701, typeof(CloudyDay))]
        [TestCase(201, typeof(RainyDay))]
        [TestCase(200, typeof(RainyDay))]
        [TestCase(199, typeof(WeatherDay))]
        [TestCase(0, typeof(WeatherDay))]
        [TestCase(-1, typeof(WeatherDay))]
        public void GetDay_NightIsFalse_ReturnsCorrectWeatherDayType(int number, Type expectedType)
        {
            var weatherNumber = new WeatherNumber() { Number = number };
            var currentWeather = new CurrentWeather() { Weather = weatherNumber };

            var day = weatherManager.GetDay(currentWeather, false);

            Assert.That(day, Is.InstanceOf(expectedType));
        }

        [Test]
        public void GetDay_NightIsTrue_ReturnsNight()
        {
            var weatherNumber = new WeatherNumber() { Number = 0 };
            var currentWeather = new CurrentWeather() { Weather = weatherNumber };

            var day = weatherManager.GetDay(currentWeather, true);

            Assert.That(day, Is.InstanceOf<Night>());
        }

        [Test]
        public void GetDay_WeatherIsNull_ReturnsWeatherDay()
        {
            var day = weatherManager.GetDay(null, false);

            Assert.That(day, Is.InstanceOf<WeatherDay>());
        }

        [Test]
        public void GetDay_WeatherNumberIsNull_ReturnsWeatherDay()
        {
            var currentWeather = new CurrentWeather();

            var day = weatherManager.GetDay(currentWeather, false);

            Assert.That(day, Is.InstanceOf<WeatherDay>());
        }
    }
}
