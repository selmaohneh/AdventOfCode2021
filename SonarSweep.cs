using AdventOfCode2021.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    internal class SonarSweep
    {
        public int Part1(string input)
        {
            List<int> depths = GetDepthsFromInput(input);

            int numberOfIncreases = GetNumberOfIncreases(depths);

            return numberOfIncreases;
        }

        public int Part2(string input)
        {
            var depths = GetDepthsFromInput(input);

            List<int> windows = GetSumsOfThreeMeasurementWindows(depths);

            int numberOfIncreases = GetNumberOfIncreases(windows);

            return numberOfIncreases;
        }

        private List<int> GetSumsOfThreeMeasurementWindows(List<int> depths)
        {
            var windows = new List<int>();

            for (int i = 0; i < depths.Count; i++)
            {
                var window = depths.Skip(i).Take(3).ToList();

                if (window.Count != 3)
                {
                    break;
                }

                int sum = window.Sum();
                windows.Add(sum);
            }

            return windows;
        }

        private List<int> GetDepthsFromInput(string input)
        {
            string[] lines = input.Split(Environment.NewLine);
            var depths = lines.Select(int.Parse).ToList();

            return depths;
        }

        private int GetNumberOfIncreases(List<int> numbers)
        {
            int numberOfIncreases = 0;

            for (int i = 0; i < numbers.Count; i++)
            {
                int currentDepth = numbers[i];

                if (i == 0)
                {
                    continue;
                }

                int previousDepth = numbers[i - 1];

                if (currentDepth > previousDepth)
                {
                    numberOfIncreases++;
                }
            }

            return numberOfIncreases;
        }
    }

    [TestClass]
    public class Day1
    {
        [TestMethod]
        public void Day1_Part1()
        {
            var sut = new SonarSweep();
            int increases = sut.Part1(Resources.Day1);
            Assert.AreEqual(1301, increases);
        }

        [TestMethod]
        public void Day2_Part2()
        {
            var sut = new SonarSweep();
            int increases = sut.Part2(Resources.Day1);
            Assert.AreEqual(1346, increases);
        }
    }
}