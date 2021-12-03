using AdventOfCode2021.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    internal class BinaryDiagnostic
    {
        public long Part1(string input)
        {
            var diagnostics = GetDiagnosticsFromInput(input);

            string mostCommonBits = String.Empty;
            string leastCommonBits = String.Empty;

            var positions = diagnostics.Select(x => x.ToArray()).ToList();

            for (int i = 0; i < positions[0].Length; i++)
            {
                MostCommonBit commonBit = GetMostCommonBit(i, positions);

                if (commonBit is MostCommonBit.Zero)
                {
                    mostCommonBits += '0';
                    leastCommonBits += '1';
                }
                else
                {
                    mostCommonBits += '1';
                    leastCommonBits += '0';
                }
            }

            long gammeRate = Convert.ToInt32(mostCommonBits, 2);
            long epsilonRate = Convert.ToInt32(leastCommonBits, 2);

            long powerConsumption = gammeRate * epsilonRate;

            return powerConsumption;
        }

        public long Part2(string input)
        {
            var diagnostics = GetDiagnosticsFromInput(input);

            int oxygenGeneratorRating = GetOxygenGeneratorRating(diagnostics);
            int co2ScrubberRating = GetCo2ScrubberRating(diagnostics);

            return oxygenGeneratorRating * co2ScrubberRating;
        }

        public enum MostCommonBit
        {
            Zero,
            Equal,
            One
        }

        private MostCommonBit GetMostCommonBit(int currentPosition, IEnumerable<char[]> positions)
        {
            var currentBits = positions.Select(x => x[currentPosition]).ToList();

            int numberOfZeros = currentBits.Count(x => x == '0');
            int numberOfOnes = currentBits.Count(x => x == '1');

            if (numberOfOnes > numberOfZeros)
            {
                return MostCommonBit.One;
            }

            if (numberOfOnes < numberOfZeros)
            {
                return MostCommonBit.Zero;
            }

            return MostCommonBit.Equal;
        }

        private int GetOxygenGeneratorRating(List<string> diagnostics)
        {
            int currentPosition = 0;
            List<char[]> positions = diagnostics.Select(x => x.ToArray()).ToList();

            while (true)
            {
                MostCommonBit commonBit = GetMostCommonBit(currentPosition, positions);

                if (commonBit is MostCommonBit.Zero)
                {
                    positions = positions.Where(x => x[currentPosition] == '0').ToList();
                }
                else
                {
                    positions = positions.Where(x => x[currentPosition] == '1').ToList();
                }

                if (positions.Count == 1)
                {
                    string co2ScrubberRatingBits = new(positions.First());

                    return Convert.ToInt32(co2ScrubberRatingBits, 2);
                }

                currentPosition++;
            }
        }

        private int GetCo2ScrubberRating(List<string> diagnostics)
        {
            int currentPosition = 0;
            List<char[]> positions = diagnostics.Select(x => x.ToArray()).ToList();

            while (true)
            {
                MostCommonBit commonBit = GetMostCommonBit(currentPosition, positions);

                if (commonBit is MostCommonBit.Zero)
                {
                    positions = positions.Where(x => x[currentPosition] == '1').ToList();
                }
                else
                {
                    positions = positions.Where(x => x[currentPosition] == '0').ToList();
                }

                if (positions.Count == 1)
                {
                    string co2ScrubberRatingBits = new(positions.First());

                    return Convert.ToInt32(co2ScrubberRatingBits, 2);
                }

                currentPosition++;
            }
        }

        private List<string> GetDiagnosticsFromInput(string input)
        {
            string[] diagnostics = input.Split(Environment.NewLine);

            return diagnostics.ToList();
        }
    }

    [TestClass]
    public class Day3
    {
        [TestMethod]
        public void Day3_Part1()
        {
            var sut = new BinaryDiagnostic();
            long result = sut.Part1(Resources.Day3);
            Assert.AreEqual(841526, result);
        }

        [TestMethod]
        public void Day3_Part2()
        {
            var sut = new BinaryDiagnostic();
            long result = sut.Part2(Resources.Day3);
            Assert.AreEqual(4790390, result);
        }
    }
}