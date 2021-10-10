using ExerciseWeather.Models;
using ExerciseWeather.Models.Exercise;
using NUnit.Framework;

namespace ExerciseWeather.UnitTests.Models.Exercise
{
    [TestFixture]
    public class ExerciseListTests
    {
        [Test]
        public void ExerciseList_WhenArgumentsAreGreaterThanZero_FirstValueIsNotRest()
        {
            var list = new ExerciseList(3);

            Assert.That(list.List[0], Is.Not.EqualTo("Rest"));
        }

        [Test]
        public void ExerciseList_WhenArgumentsAreGreaterThanZero_SecondValueIsRest()
        {
            var list = new ExerciseList(3);

            Assert.That(list.List[1], Is.EqualTo("Rest"));
        }

        [Test]
        public void ExerciseList_WhenArgumentsAreGreaterThanZero_ExercisesDoNotRepeat()
        {
            var list = new ExerciseList(3);

            Assert.That(list.List[0], Is.Not.EqualTo(list.List[2]));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void ExerciseList_WhenArgumentsAreGreaterThanZero_ReturnsDoubleLengthListOfExercises(int numberOfExercises)
        {
            var list = new ExerciseList(numberOfExercises);

            Assert.That(list.List.Count, Is.EqualTo(numberOfExercises * 2));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ExerciseList_WhenArgumentsAreEqualToOrLessThanZero_ReturnsEmptyList(int numberOfExercises)
        {
            var list = new ExerciseList(numberOfExercises);

            Assert.That(list.List.Count, Is.EqualTo(0));
        }
    }
}
