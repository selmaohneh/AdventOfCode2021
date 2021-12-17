namespace AdventOfCode2021
{
    internal class AStar
    {
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
}