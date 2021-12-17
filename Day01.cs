using AdventOfCode2021.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    internal class Day01
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

        public List<int> GetSumsOfThreeMeasurementWindows(List<int> depths)
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

        public List<int> GetDepthsFromInput(string input)
        {
            string[] lines = input.Split(Environment.NewLine);
            var depths = lines.Select(int.Parse).ToList();

            return depths;
        }

        public int GetNumberOfIncreases(List<int> numbers)
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
    public class Day01Tests
    {
        [TestMethod]
        public void ParseInput()
        {
            var sut = new Day01();

            var depths = sut.GetDepthsFromInput(Resources.Day01_Debug);

            Assert.AreEqual(10, depths.Count);
            Assert.AreEqual(199, depths.First());
            Assert.AreEqual(263, depths.Last());
        }

        [TestMethod]
        public void CountIncreases()
        {
            var sut = new Day01();

            var input = new List<int>
            {
                199,
                200,
                208,
                210,
                200,
                207,
                240,
                269,
                260,
                263
            };

            int increases = sut.GetNumberOfIncreases(input);

            Assert.AreEqual(7, increases);
        }

        [TestMethod]
        public void Part1_Debug()
        {
            var sut = new Day01();
            int result = sut.Part1(Resources.Day01_Debug);
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void Part1()
        {
            var sut = new Day01();
            int result = sut.Part1(Resources.Day01);
            Assert.AreEqual(1301, result);
        }

        [TestMethod]
        public void GetSumOfThreeMeasurementWindow()
        {
            var sut = new Day01();

            var input = new List<int>
            {
                199,
                200,
                208,
                210,
                200,
                207,
                240,
                269,
                260,
                263
            };

            var result = sut.GetSumsOfThreeMeasurementWindows(input);

            Assert.AreEqual(8, result.Count);
            Assert.AreEqual(607, result.First());
            Assert.AreEqual(792, result.Last());
        }

        [TestMethod]
        public void Part2_Debug()
        {
            var sut = new Day01();
            int result = sut.Part2(Resources.Day01_Debug);
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Part2()
        {
            var sut = new Day01();
            int result = sut.Part2(Resources.Day01);
            Assert.AreEqual(1346, result);
        }
    }
}