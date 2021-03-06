using AdventOfCode2021.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021;

internal class Day05
{
    public int Part1(string input)
    {
        List<Line> lines = GetLinesFromInput(input);

        var allPoints = lines.SelectMany(x => x.GetPointsOfLine(includeDiagonals: false)).ToList();

        return GetDuplicatesCount(allPoints);
    }

    private int GetDuplicatesCount(List<Point> allPoints)
    {
        return allPoints.GroupBy(p => new
                         {
                             p.X,
                             p.Y
                         })
                        .Count(g => g.Count() >= 2);
    }

    private List<Line> GetLinesFromInput(string input)
    {
        string[] inputLines = input.Split(Environment.NewLine);

        var lines = new List<Line>();

        foreach (string inputLine in inputLines)
        {
            string[] pointInputs = inputLine.Split(" -> ");
            string pointStartInput = pointInputs[0];
            string pointEndInput = pointInputs[1];
            string[] pointStartValues = pointStartInput.Split(",");
            string[] pointEndValues = pointEndInput.Split(",");
            int pointStartX = Int32.Parse(pointStartValues[0]);
            int pointStartY = Int32.Parse(pointStartValues[1]);
            int pointEndX = Int32.Parse(pointEndValues[0]);
            int pointEndY = Int32.Parse(pointEndValues[1]);

            var pointStart = new Point(pointStartX, pointStartY);
            var pointEnd = new Point(pointEndX, pointEndY);

            var line = new Line(pointStart, pointEnd);
            lines.Add(line);
        }

        return lines;
    }

    public int Part2(string input)
    {
        List<Line> lines = GetLinesFromInput(input);

        var allPoints = lines.SelectMany(x => x.GetPointsOfLine(includeDiagonals: true)).ToList();

        return GetDuplicatesCount(allPoints);
    }

    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Line
    {
        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public IEnumerable<Point> GetPointsOfLine(bool includeDiagonals)
        {
            var points = new List<Point>();

            if (Start.X == End.X)
            {
                int yMin = Math.Min(Start.Y, End.Y);
                int yMax = Math.Max(Start.Y, End.Y);

                for (int y = yMin; y <= yMax; y++)
                {
                    var point = new Point(Start.X, y);
                    points.Add(point);
                }
            }
            else if (Start.Y == End.Y)
            {
                int xMin = Math.Min(Start.X, End.X);
                int xMax = Math.Max(Start.X, End.X);

                for (int x = xMin; x <= xMax; x++)
                {
                    var point = new Point(x, Start.Y);
                    points.Add(point);
                }
            }
            else if (includeDiagonals)
            {
                int x = Start.X;
                int y = Start.Y;

                while (true)
                {
                    var point = new Point(x, y);
                    points.Add(point);

                    if (x == End.X && y == End.Y)
                    {
                        return points;
                    }

                    if (Start.X < End.X)
                    {
                        x++;
                    }
                    else
                    {
                        x--;
                    }

                    if (Start.Y < End.Y)
                    {
                        y++;
                    }
                    else
                    {
                        y--;
                    }
                }
            }

            return points;
        }

        public Point Start { get; set; }
        public Point End { get; set; }
    }

    [TestClass]
    public class Day05Tests
    {
        [TestMethod]
        public void Part1_Debug()
        {
            var sut = new Day05();
            int result = sut.Part1(Resources.Day05_Debug);
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Part1()
        {
            var sut = new Day05();
            int result = sut.Part1(Resources.Day05);
            Assert.AreEqual(6397, result);
        }

        [TestMethod]
        public void Part2_Debug()
        {
            var sut = new Day05();
            int result = sut.Part2(Resources.Day05_Debug);
            Assert.AreEqual(12, result);
        }

        [TestMethod]
        public void Part2()
        {
            var sut = new Day05();
            int result = sut.Part2(Resources.Day05);
            Assert.AreEqual(2233, result);
        }
    }
}