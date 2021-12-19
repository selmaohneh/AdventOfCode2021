using AdventOfCode2021.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    internal class Day15
    {
        public int Part1(string input)
        {
            string[] lines = input.Split(Environment.NewLine);

            var graph = new Dictionary<Point, Node>();

            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];

                for (int x = 0; x < line.Length; x++)
                {
                    var point = new Point(x, y);
                    var node = new Node(Int32.Parse(line[x].ToString()));
                    graph.Add(point, node);
                }
            }

            var start = new Point(0, 0);

            int maxX = graph.Keys.Max(x => x.X);
            int maxY = graph.Keys.Max(x => x.Y);
            var end = new Point(maxX, maxY);

            var aStar = new AStar();
            var path = aStar.FindPath(graph, start, end);

            int risk = path.Skip(1).Sum(x => x.Cost);

            return risk;
        }

        public int Part2(string input)
        {
            string[] lines = input.Split(Environment.NewLine);

            var graph = new Dictionary<Point, Node>();

            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];

                for (int x = 0; x < line.Length; x++)
                {
                    var point = new Point(x, y);
                    var node = new Node(Int32.Parse(line[x].ToString()));
                    graph.Add(point, node);
                }
            }

            int maxX = graph.Keys.Max(x => x.X);
            int maxY = graph.Keys.Max(x => x.Y);

            for (int y = 0; y < (maxY + 1) * 5; y++)
            {
                for (int x = 0; x < (maxX + 1) * 5; x++)
                {
                    if (x <= maxX && y <= maxY)
                    {
                        continue;
                    }

                    int xOffset = 0;

                    if (x > maxX)
                    {
                        xOffset = maxX + 1;
                    }

                    int yOffset = 0;

                    if (y > maxY)
                    {
                        yOffset = maxY + 1;
                    }

                    Node template;

                    if (graph.ContainsKey(new Point(x - xOffset, y)))
                    {
                        template = graph[new Point(x - xOffset, y)];
                    }
                    else
                    {
                        template = graph[new Point(x, y - yOffset)];
                    }

                    int newRisk = template.Cost + 1;

                    if (newRisk > 9)
                    {
                        newRisk = 1;
                    }

                    var node = new Node(newRisk);
                    graph.Add(new Point(x, y), node);
                }
            }

            var start = new Point(0, 0);

            maxX = graph.Keys.Max(x => x.X);
            maxY = graph.Keys.Max(x => x.Y);
            var end = new Point(maxX, maxY);

            var aStar = new AStar();
            var path = aStar.FindPath(graph, start, end);

            int risk = path.Skip(1).Sum(x => x.Cost);

            return risk;
        }
    }

    [TestClass]
    public class Day15Tests
    {
        [TestMethod]
        public void Part1_Debug()
        {
            var sut = new Day15();
            int result = sut.Part1(Resources.Day15_Debug);
            Assert.AreEqual(40, result);
        }

        [TestMethod]
        public void Part1()
        {
            var sut = new Day15();
            int result = sut.Part1(Resources.Day15);
            Assert.AreEqual(390, result);
        }

        [TestMethod]
        public void Part2_Debug()
        {
            var sut = new Day15();
            int result = sut.Part2(Resources.Day15_Debug);
            Assert.AreEqual(315, result);
        }

        [TestMethod]
        public void Part2()
        {
            var sut = new Day15();
            int result = sut.Part2(Resources.Day15);
            Assert.AreEqual(2814, result);
        }
    }
}