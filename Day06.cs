using AdventOfCode2021.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    internal class Day06
    {
        public int Part1(string input)
        {
            var fishes = input.Split(",").Select(Int32.Parse).ToList();

            for (int dayIndex = 0; dayIndex < 80; dayIndex++)
            {
                int zeroFishes = fishes.Count(x => x == 0);

                for (int i = 0; i < zeroFishes; i++)
                {
                    fishes.Add(9);
                }

                for (int fishIndex = 0; fishIndex < fishes.Count; fishIndex++)
                {
                    fishes[fishIndex] = --fishes[fishIndex];

                    if (fishes[fishIndex] < 0)
                    {
                        fishes[fishIndex] = 6;
                    }
                }
            }

            return fishes.Count;
        }

        public long Part2(string input)
        {
            var initialFishes = input.Split(",").Select(Int32.Parse).ToList();

            long[] countsPerNumber = new long[9];

            foreach (int initialFish in initialFishes)
            {
                countsPerNumber[initialFish]++;
            }

            for (int dayIndex = 0; dayIndex < 256; dayIndex++)
            {
                long births = countsPerNumber[0];

                for (int numberIndex = 0; numberIndex < countsPerNumber.Length; numberIndex++)
                {
                    if (numberIndex == 8)
                    {
                        countsPerNumber[8] = births;
                        countsPerNumber[6] += births;

                        continue;
                    }

                    countsPerNumber[numberIndex] = countsPerNumber[numberIndex + 1];
                }
            }

            return countsPerNumber.Sum();
        }
    }

    [TestClass]
    public class Day06Tests
    {
        [TestMethod]
        public void Part1_Debug()
        {
            var sut = new Day06();
            int result = sut.Part1(Resources.Day06_Debug);
            Assert.AreEqual(5934, result);
        }

        [TestMethod]
        public void Part1()
        {
            var sut = new Day06();
            int result = sut.Part1(Resources.Day06);
            Assert.AreEqual(372300, result);
        }

        [TestMethod]
        public void Part2_Debug()
        {
            var sut = new Day06();
            long result = sut.Part2(Resources.Day06_Debug);
            Assert.AreEqual(26984457539, result);
        }

        [TestMethod]
        public void Day6_Part2()
        {
            var sut = new Day06();
            long result = sut.Part2(Resources.Day06);
            Assert.AreEqual(1675781200288, result);
        }
    }
}