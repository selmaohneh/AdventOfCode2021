using AdventOfCode2021.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    internal class LanternFish
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

        public float Part2(string input)
        {
            var initialFishes = input.Split(",").Select(Int32.Parse).ToList();

            float[] countsPerNumber = new float[9];

            foreach (int initialFish in initialFishes)
            {
                countsPerNumber[initialFish]++;
            }

            for (int dayIndex = 0; dayIndex < 256; dayIndex++)
            {
                float births = countsPerNumber[0];

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
    public class Day6
    {
        [TestMethod]
        public void Day6_Part1()
        {
            var sut = new LanternFish();

            int result = sut.Part1(Resources.Day6);

            Assert.AreEqual(372300, result);
        }

        [TestMethod]
        public void Day6_Part2()
        {
            var sut = new LanternFish();

            float result = sut.Part2(Resources.Day6);

            Assert.AreEqual(1675781200288, result);
        }
    }
}