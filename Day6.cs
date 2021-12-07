using AdventOfCode2021.Properties;

namespace AdventOfCode2021
{
    internal class Day6 : IAdeventOfCodeDay
    {
        public string Input { get; } = Resources.Day6;

        public string Part1()
        {
            var fishes = Input.Split(",").Select(Int32.Parse).ToList();

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

            return fishes.Count.ToString();
        }

        public string Part2()
        {
            var initialFishes = Input.Split(",").Select(Int32.Parse).ToList();

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

            return countsPerNumber.Sum().ToString();
        }
    }
}