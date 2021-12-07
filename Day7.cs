using AdventOfCode2021.Properties;

namespace AdventOfCode2021
{
    internal class Day7 : IAdeventOfCodeDay
    {
        public string Input { get; } = Resources.Day7;

        public string Part1()
        {
            var initialPositions = Input.Split(",").Select(Int32.Parse).ToList();

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

            return fuelConsumptionsPerTargetPosition.Min().ToString();
        }

        public string Part2()
        {
            var initialPositions = Input.Split(",").Select(Int32.Parse).ToList();

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

            return fuelConsumptionsPerTargetPosition.Min().ToString();
        }
    }
}