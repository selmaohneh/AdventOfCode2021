using AdventOfCode2021.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    internal class Day09
    {
        public int Part1(string input)
        {
            IDictionary<Point, int> map = CreateMap(input);

            HashSet<Point> lowPoints = FindLowPoints(map);

            var riskLevels = lowPoints.Select(x => map[x] + 1);

            return riskLevels.Sum();
        }

        public int Part2(string input)
        {
            IDictionary<Point, int> map = CreateMap(input);

            HashSet<Point> lowPoints = FindLowPoints(map);

            List<int> basinSizes = GetBasinSizes(lowPoints, map);

            int result = GetProductOfBiggestThreeBasins(basinSizes);

            return result;
        }

        private int GetProductOfBiggestThreeBasins(List<int> basinSizes)
        {
            int result = 1;
            var threeBiggestBasins = basinSizes.OrderByDescending(x => x).Take(3);

            foreach (int basin in threeBiggestBasins)
            {
                result *= basin;
            }

            return result;
        }

        private List<int> GetBasinSizes(HashSet<Point> lowPoints, IDictionary<Point, int> map)
        {
            var basinSizes = new List<int>();

            foreach (Point lowPoint in lowPoints)
            {
                var basinPoints = new HashSet<Point>();

                var queue = new Queue<Point>();
                queue.Enqueue(lowPoint);

                while (queue.Any())
                {
                    Point currentPoint = queue.Dequeue();

                    if (!basinPoints.Any(x => x.X == currentPoint.X && x.Y == currentPoint.Y))
                    {
                        basinPoints.Add(currentPoint);
                    }

                    var adjacents = currentPoint.GetAdjacents(map);

                    var currentBasinPoints = adjacents.Where(x => map[x] > map[currentPoint] && map[x] != 9).ToList();

                    foreach (Point currentBasinPoint in currentBasinPoints)
                    {
                        queue.Enqueue(currentBasinPoint);
                    }
                }

                basinSizes.Add(basinPoints.Count);
            }

            return basinSizes;
        }

        private IDictionary<Point, int> CreateMap(string input)
        {
            var map = new Dictionary<Point, int>();
            string[] lines = input.Split(Environment.NewLine);

            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];
                var heightsOfLine = line.ToCharArray().Select(x => Int32.Parse(x.ToString())).ToList();

                for (int x = 0; x < heightsOfLine.Count; x++)
                {
                    int height = heightsOfLine[x];
                    map.Add(new Point(x, y), height);
                }
            }

            return map;
        }

        private HashSet<Point> FindLowPoints(IDictionary<Point, int> map)
        {
            var lowPoints = new HashSet<Point>();

            foreach (KeyValuePair<Point, int> point in map)
            {
                var adjacents = point.Key.GetAdjacents(map);

                if (adjacents.All(adjacent => map[adjacent] > map[point.Key]))
                {
                    lowPoints.Add(point.Key);
                }
            }

            return lowPoints;
        }

        public struct Point
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; }
            public int Y { get; }

            public HashSet<Point> GetAdjacents(IDictionary<Point, int> map)
            {
                var topPoint = new Point(X, Y - 1);
                var botPoint = new Point(X, Y + 1);
                var leftPoint = new Point(X - 1, Y);
                var rightPoint = new Point(X + 1, Y);

                var adjacentCandidates = new HashSet<Point>
                {
                    topPoint,
                    botPoint,
                    leftPoint,
                    rightPoint
                };

                var adjacents = new HashSet<Point>();

                foreach (Point adjacentCandidate in adjacentCandidates)
                {
                    if (map.ContainsKey(adjacentCandidate))
                    {
                        adjacents.Add(adjacentCandidate);
                    }
                }

                return adjacents;
            }
        }
    }

    [TestClass]
    public class Day09Tests
    {
        [TestMethod]
        public void Part1_Debug()
        {
            var sut = new Day09();
            int result = sut.Part1(Resources.Day09_Debug);
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void Part1()
        {
            var sut = new Day09();
            int result = sut.Part1(Resources.Day09);
            Assert.AreEqual(577, result);
        }

        [TestMethod]
        public void Part2_Debug()
        {
            var sut = new Day09();
            int result = sut.Part2(Resources.Day09_Debug);
            Assert.AreEqual(1134, result);
        }

        [TestMethod]
        public void Part2()
        {
            var sut = new Day09();
            int result = sut.Part2(Resources.Day09);
            Assert.AreEqual(1069200, result);
        }
    }
}