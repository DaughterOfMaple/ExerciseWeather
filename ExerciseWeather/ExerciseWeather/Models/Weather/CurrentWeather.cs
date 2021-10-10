using System.Xml.Serialization;

namespace ExerciseWeather.Models.Weather
{
    [XmlRoot("current")]
    public class CurrentWeather
    {
        [XmlElement("weather")]
        public WeatherNumber Weather { get; set; }
    }
}
