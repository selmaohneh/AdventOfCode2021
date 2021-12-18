using AdventOfCode2021.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    internal class Day07
    {
        public int Part1(string input)
        {
            var initialPositions = input.Split(",").Select(Int32.Parse).ToList();

            int minPosition = initialPositions.Min();
            int maxPosition = initialPositions.Max();

            var fuelConsumptionsPerTargetPosition = new List<int>();

            for (int targetPosition = minPosition; targetPosition <= maxPosition; targetPosition++)
            {
                int totalFuelConsumption = 0;

                foreach (int position in initialPositions)
                {
                    int currentFuelConsumption = Math.Abs(targetPosition - position);
                    totalFuelConsumption += currentFuelConsumption;
                }

                fuelConsumptionsPerTargetPosition.Add(totalFuelConsumption);
            }

            return fuelConsumptionsPerTargetPosition.Min();
        }

        public int Part2(string input)
        {
            var initialPositions = input.Split(",").Select(Int32.Parse).ToList();

            int minPosition = initialPositions.Min();
            int maxPosition = initialPositions.Max();

            var fuelConsumptionsPerTargetPosition = new List<int>();

            for (int targetPosition = minPosition; targetPosition <= maxPosition; targetPosition++)
            {
                int totalFuelConsumption = 0;

                foreach (int position in initialPositions)
                {
                    int totalDistance = Math.Abs(targetPosition - position);

                    for (int i = 0; i < totalDistance; i++)
                    {
                        int fuelCosts = i + 1;
                        totalFuelConsumption += fuelCosts;
                    }
                }

                fuelConsumptionsPerTargetPosition.Add(totalFuelConsumption);
            }

            return fuelConsumptionsPerTargetPosition.Min();
        }
    }

    [TestClass]
    public class Day07Tests
    {
        [TestMethod]
        public void Part1_Debug()
        {
            var sut = new Day07();
            int result = sut.Part1(Resources.Day07_Debug);
            Assert.AreEqual(37, result);
        }

        [TestMethod]
        public void Part1()
        {
            var sut = new Day07();
            int result = sut.Part1(Resources.Day07);
            Assert.AreEqual(328262, result);
        }

        [TestMethod]
        public void Part2_Debug()
        {
            var sut = new Day07();
            int result = sut.Part2(Resources.Day07_Debug);
            Assert.AreEqual(168, result);
        }

        [TestMethod]
        public void Day7_Part2()
        {
            var sut = new Day07();
            int result = sut.Part2(Resources.Day07);
            Assert.AreEqual(90040997, result);
        }
    }
}