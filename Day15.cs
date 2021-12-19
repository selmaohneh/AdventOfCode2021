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

            var path = FindPath(graph, start, end);

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

            var path = FindPath(graph, start, end);

            int risk = path.Skip(1).Sum(x => x.Cost);

            return risk;
        }

        public List<Node> FindPath(Dictionary<Point, Node> graph, Point startNode, Point targetPoint)
        {
            // contains all nodes that were already visited
            var visitedPoints = new HashSet<Point>();

            // contains all nodes that need to be explored with a corresponding priority.
            var remainingPoints = new Dictionary<Point, int>();

            foreach (Node node in graph.Values)
            {
                node.DistanceFromStart = Int32.MaxValue;
            }

            // to get started: add the starting node to the list of nodes we want to explore.
            remainingPoints.Add(startNode, 0);

            while (remainingPoints.Any())
            {
                // get the most promising node, i.e. the one with the lowest priority
                Point currentPoint = remainingPoints.MinBy(x => x.Value).Key;
                Node currentNode = graph[currentPoint];

                remainingPoints.Remove(currentPoint);

                // did we find the target?
                if (Equals(currentPoint, targetPoint))
                {
                    Node targetNode = graph[targetPoint];

                    return GetShortestPath(targetNode);
                }

                visitedPoints.Add(currentPoint);

                // explore all neighbours if this node
                var neighbors = currentPoint.GetNeighbours(graph);

                foreach (Point neighborPoint in neighbors)
                {
                    // did we already look at this neighbour?
                    if (visitedPoints.Contains(neighborPoint))
                    {
                        continue;
                    }

                    Node neighbor = graph[neighborPoint];

                    // what are the total costs we need to get this neighbor?
                    int totalCost = currentNode.DistanceFromStart + neighbor.Cost;

                    // we still need to explore this neighbor but the neighbor already has a shorter path.
                    if (remainingPoints.ContainsKey(neighborPoint) && totalCost >= neighbor.DistanceFromStart)
                    {
                        continue;
                    }

                    // this path to this neighbor is shorter than before!
                    // update the data for the neighbor.
                    neighbor.Predecessor = currentNode;
                    neighbor.DistanceFromStart = totalCost;

                    // calculate the priority, i.e. how promising the neighbor is.
                    int priority = totalCost + neighborPoint.GetManhattenDistanceToTarget(targetPoint);

                    // we already plannend to explore the neighbor but now it has higher new priority.
                    if (remainingPoints.ContainsKey(neighborPoint))
                    {
                        remainingPoints[neighborPoint] = priority;
                    }

                    // we see this neighbor for the first time. add it to the nodes to explore in future.
                    else
                    {
                        remainingPoints.Add(neighborPoint, priority);
                    }
                }
            }

            throw new Exception("Can not find a path to the target");
        }

        private List<Node> GetShortestPath(Node targetNode)
        {
            var path = new List<Node>
            {
                targetNode
            };

            Node u = targetNode;

            while (u.Predecessor != null)
            {
                u = u.Predecessor;

                path.Insert(0, u);
            }

            return path;
        }
    }

    internal struct Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int GetManhattenDistanceToTarget(Point targetPoint)
        {
            return Math.Abs(X - targetPoint.X) + Math.Abs(Y - targetPoint.Y);
        }

        public List<Point> GetNeighbours(Dictionary<Point, Node> graph)
        {
            var neighbours = new List<Point>();

            var rightNeighbour = new Point(X + 1, Y);
            var leftNeighbour = new Point(X - 1, Y);
            var topNeighbour = new Point(X, Y - 1);
            var botNeighbhour = new Point(X, Y + 1);

            if (graph.ContainsKey(rightNeighbour))
            {
                neighbours.Add(rightNeighbour);
            }

            if (graph.ContainsKey(leftNeighbour))
            {
                neighbours.Add(leftNeighbour);
            }

            if (graph.ContainsKey(topNeighbour))
            {
                neighbours.Add(topNeighbour);
            }

            if (graph.ContainsKey(botNeighbhour))
            {
                neighbours.Add(botNeighbhour);
            }

            return neighbours;
        }
    }

    internal class Node
    {
        public int Cost { get; }

        public Node(int cost)
        {
            Cost = cost;
        }

        public int DistanceFromStart { get; set; }
        public Node Predecessor { get; set; }
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