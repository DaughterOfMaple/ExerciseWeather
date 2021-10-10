using ExerciseWeather.Models.Weather;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExerciseWeather.ViewModels
{
    public class RestService : IRestService
    {
        private readonly HttpClient client;

        public RestService()
        {
            client = new HttpClient();
        }

        public async Task<CurrentWeather> FetchWeatherData(string query)
        {
            try
            {
                var response = await client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Deserialize(content);
                }
            }
            catch (WebException e)
            {
                Debug.WriteLine($"\t\tERROR {e.Message} \n The web connection failed."); //TODO: Add alert message
            }
            catch (Exception e)
            {
                Debug.WriteLine($"\t\tERROR {e.Message} \n The weather data did not fetch.");
            }
            return new CurrentWeather();
        }

        private CurrentWeather Deserialize(string input)
        {
            CurrentWeather weather;
            var serializer = new XmlSerializer(typeof(CurrentWeather));

            using (StringReader reader = new StringReader(input))
            {
                weather = (CurrentWeather)serializer.Deserialize(reader);
            }
            return weather;
        }
    }
}
