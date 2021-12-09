using AdventOfCode2021.Properties;

namespace AdventOfCode2021
{
    internal class Day9 : IAdeventOfCodeDay
    {
        public string Input => Resources.Day9;

        public string Part1()
        {
            List<Point> points = GetPoints();

            List<Point> lowPoints = FindLowPoints(points);

            var riskLevels = lowPoints.Select(x => x.Height + 1);

            return riskLevels.Sum().ToString();
        }

        public string Part2()
        {
            List<Point> points = GetPoints();

            List<Point> lowPoints = FindLowPoints(points);

            List<int> basinSizes = GetBasinSizes(lowPoints, points);

            int result = GetProductOfBiggestThreeBasins(basinSizes);

            return result.ToString();
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

        private List<int> GetBasinSizes(List<Point> lowPoints, List<Point> points)
        {
            var basinSizes = new List<int>();

            foreach (Point lowPoint in lowPoints)
            {
                var basinPoints = new List<Point>();

                var queue = new Queue<Point>();
                queue.Enqueue(lowPoint);

                while (queue.Any())
                {
                    Point currentPoint = queue.Dequeue();

                    if (!basinPoints.Any(x => x.X == currentPoint.X && x.Y == currentPoint.Y))
                    {
                        basinPoints.Add(currentPoint);
                    }

                    var adjacents = GetAdjacents(points, currentPoint).ToList();

                    var currentBasinPoints = adjacents.Where(x => x.Height > currentPoint.Height && x.Height != 9).ToList();

                    foreach (Point currentBasinPoint in currentBasinPoints)
                    {
                        queue.Enqueue(currentBasinPoint);
                    }
                }

                basinSizes.Add(basinPoints.Count);
            }

            return basinSizes;
        }

        private List<Point> GetPoints()
        {
            var points = new List<Point>();
            string[] lines = Input.Split(Environment.NewLine);

            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];
                var heightsOfLine = line.ToCharArray().Select(x => Int32.Parse(x.ToString())).ToList();

                for (int x = 0; x < heightsOfLine.Count; x++)
                {
                    int height = heightsOfLine[x];
                    points.Add(new Point(height, x, y));
                }
            }

            return points;
        }

        private List<Point> FindLowPoints(List<Point> points)
        {
            var lowPoints = new List<Point>();

            foreach (Point point in points)
            {
                var adjacents = GetAdjacents(points, point);

                if (adjacents.All(adjacent => adjacent.Height > point.Height))
                {
                    lowPoints.Add(point);
                }
            }

            return lowPoints;
        }

        private IEnumerable<Point> GetAdjacents(List<Point> points, Point point)
        {
            var adjacents = new List<Point>();

            Point? topPoint = points.SingleOrDefault(x => x.X == point.X && x.Y == point.Y - 1);

            if (topPoint != null)
            {
                adjacents.Add(topPoint);
            }

            Point? botPoint = points.SingleOrDefault(x => x.X == point.X && x.Y == point.Y + 1);

            if (botPoint != null)
            {
                adjacents.Add(botPoint);
            }

            Point? leftPoint = points.SingleOrDefault(x => x.X == point.X - 1 && x.Y == point.Y);

            if (leftPoint != null)
            {
                adjacents.Add(leftPoint);
            }

            Point? rightPoint = points.SingleOrDefault(x => x.X == point.X + 1 && x.Y == point.Y);

            if (rightPoint != null)
            {
                adjacents.Add(rightPoint);
            }

            return adjacents;
        }

        public class Point
        {
            public Point(int height, int x, int y)
            {
                Height = height;
                X = x;
                Y = y;
            }

            public int Height { get; set; }
            public int X { get; set; }
            public int Y { get; set; }

            public override string ToString()
            {
                return $"{X},{Y}: {Height}";
            }
        }
    }
}