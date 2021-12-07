using AdventOfCode2021.Properties;

namespace AdventOfCode2021
{
    internal class Day1 : IAdeventOfCodeDay
    {
        public string Input { get; } = Resources.Day1;

        public string Part1()
        {
            List<int> depths = GetDepthsFromInput(Input);

            int numberOfIncreases = GetNumberOfIncreases(depths);

            return numberOfIncreases.ToString();
        }

        public string Part2()
        {
            var depths = GetDepthsFromInput(Input);

            List<int> windows = GetSumsOfThreeMeasurementWindows(depths);

            int numberOfIncreases = GetNumberOfIncreases(windows);

            return numberOfIncreases.ToString();
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
}