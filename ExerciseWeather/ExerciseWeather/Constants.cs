using System;
using System.IO;
using Xamarin.Forms;

namespace ExerciseWeather
{
    public static class Constants
    {
        // SQLite database values
        public const string DatabaseFilename = "ExWeatherSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

        // OpenWeatherMap API strings
        public const string APIWeatherStandardStart = "https://api.openweathermap.org/data/2.5/weather";
        public const string APIWeatherKey = "ADD_YOUR_API_KEY_HERE";
        public const string APIWeatherUnits = "metric";
        public const string APIWeatherMode = "xml";

        public static string APIWeatherString
        {
            get
            {
                return $"{APIWeatherStandardStart}" +
                   $"?appid={APIWeatherKey}" +
                   $"&units={APIWeatherUnits}" +
                   $"&mode={APIWeatherMode}" +
                   $"&q=";
            }
        }

        // Default username
        public static string user = "defaultUser";

        // Start button styles
        public const string Start = "Start!";
        public const string Pause = "Pause";
        public static Color StartBtnGreen = Color.FromHex("#d9003c01");
        public static Color StartBtnRed = Color.FromHex("#d9e28743");

        // 'Preferences' keys
        public const string City = "City";
        public const string ExerciseTime = "ExerciseTime";
        public const string RestTime = "RestTime";
        public const string NumberofExercises = "NumberOfExercises";
        public const string IsSimple = "IsSimpleBackground";
        public const string IsNight = "IsNightMode";
        public const string Rest = "Rest";
    }
}
