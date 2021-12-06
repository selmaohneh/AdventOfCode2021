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

        public int Part2(string input)
        {
            var initialFishes = input.Split(",").Select(Int32.Parse).ToList();

            File.WriteAllLines("currentfile.txt", initialFishes.Select(x => x.ToString()));

            for (int dayIndex = 0; dayIndex < 256; dayIndex++)
            {
                int zeroFishes = CountZeros();

                CreateFileWithUpdatedFishNumbers(zeroFishes);

                File.Replace("newfile.txt", "currentfile.txt", null);
            }

            int allFish = 0;

            using StreamReader reader = File.OpenText("currentfile.txt");

            while (reader.ReadLine() != null)
            {
                allFish++;
            }

            return allFish;
        }

        private static void CreateFileWithUpdatedFishNumbers(int zeroFishes)
        {
            File.Create("newfile.txt").Close();

            using (StreamReader reader = File.OpenText("currentfile.txt"))
            {
                string line = String.Empty;

                while ((line = reader.ReadLine()) != null)
                {
                    int fish = Int32.Parse(line);

                    int fishNew = fish - 1;

                    if (fishNew < 0)
                    {
                        fishNew = 6;
                    }

                    using (FileStream stream = File.Open("newfile.txt", FileMode.Append))
                    {
                        using (var writer = new StreamWriter(stream))
                        {
                            writer.WriteLine(fishNew);
                        }
                    }
                }
            }

            using (FileStream stream = File.Open("newfile.txt", FileMode.Append))
            {
                using (var writer = new StreamWriter(stream))
                {
                    for (int i = 0; i < zeroFishes; i++)
                    {
                        writer.WriteLine(8);
                    }
                }
            }
        }

        private static int CountZeros()
        {
            int zeroFishes = 0;

            using (StreamReader reader = File.OpenText("currentfile.txt"))
            {
                string line = String.Empty;

                while ((line = reader.ReadLine()) != null)
                {
                    int fish = Int32.Parse(line);

                    if (fish == 0)
                    {
                        zeroFishes++;
                    }
                }
            }

            return zeroFishes;
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

            int result = sut.Part2(Resources.Day6);

            Assert.AreEqual(1675781200288, result);
        }
    }
}