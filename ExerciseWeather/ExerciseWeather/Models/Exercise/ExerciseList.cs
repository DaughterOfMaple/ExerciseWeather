using System;
using System.Collections.Generic;

namespace ExerciseWeather.Models.Exercise
{
    public class ExerciseList
    {
        private readonly string[] exerciseLibrary = {
            "Plank",
            "Sit-ups",
            "Squats",
            "Push-ups",
            "Burpees",
            "Star jumps",
            "Side plank",
            "Run",
            "Mountain climbers",
            "High knees",
            "Lunges",
            "Wide arm push-ups",
            "Diamond push-ups",
            "Crunches",
            "Shadow boxing",
            "Fast feet"
        };

        public List<string> List { get; private set; }

        public ExerciseList()
        {
            List = new List<string>();
        }

        public ExerciseList(int numberOfExercises)
        {
            List = new List<string>();
            var updatedLibrary = new List<string>(exerciseLibrary);
            var randomNumber = new Random();

            for (int i = 0; i < numberOfExercises; i += 1)
            {
                if (updatedLibrary.Count == 0)
                    updatedLibrary = new List<string>(exerciseLibrary);

                int index = randomNumber.Next(0, updatedLibrary.Count - 1);
                List.Add(updatedLibrary[index]);
                updatedLibrary.RemoveAt(index);
                List.Add(Constants.Rest);
            }
        }
    }
}
