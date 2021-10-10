using System.Xml;
using System.Xml.Serialization;

namespace ExerciseWeather.Models.Weather
{
    public class WeatherNumber
    {
        [XmlAttribute("number")]
        public int Number { get; set; }
    }
}
