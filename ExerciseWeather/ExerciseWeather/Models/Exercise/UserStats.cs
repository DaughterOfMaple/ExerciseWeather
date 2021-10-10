using SQLite;

namespace ExerciseWeather.Models.Exercise
{
    public class UserStats
    {
        [PrimaryKey]
        public string ID { get; set; }

        public int CurrentStreak { get; set; }
        public int LongestStreak { get; set; }
        public int TotalWorkouts { get; set; }
        public string LastWorkout { get; set; }
    }
}
