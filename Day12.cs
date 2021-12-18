using AdventOfCode2021.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    internal class Day12
    {
        public int Part1(string input)
        {
            Dictionary<Node, HashSet<Node>> graph = CreateGraph(input);

            ExplorePath(new Node("start"),
                        new Node("end"),
                        graph,
                        new HashSet<Node>());

            return _count;
        }

        public int Part2(string input)
        {
            Dictionary<Node, HashSet<Node>> graph = CreateGraph(input);

            ExplorePath2(new Node("start"),
                         new Node("end"),
                         graph,
                         new HashSet<Node>(),
                         new HashSet<Node>());

            return _count;
        }

        private Dictionary<Node, HashSet<Node>> CreateGraph(string input)
        {
            string[] lines = input.Split(Environment.NewLine);

            var graph = new Dictionary<Node, HashSet<Node>>();

            foreach (string line in lines)
            {
                string[] splitLine = line.Split("-");

                var nodeA = new Node(splitLine[0]);
                var nodeB = new Node(splitLine[1]);

                if (graph.ContainsKey(nodeA))
                {
                    graph[nodeA].Add(nodeB);
                }
                else
                {
                    graph.Add(nodeA,
                              new HashSet<Node>
                              {
                                  nodeB
                              });
                }

                if (graph.ContainsKey(nodeB))
                {
                    graph[nodeB].Add(nodeA);
                }
                else
                {
                    graph.Add(nodeB,
                              new HashSet<Node>
                              {
                                  nodeA
                              });
                }
            }

            return graph;
        }

        private int _count;

        private void ExplorePath(Node currentNode, Node targetNode, Dictionary<Node, HashSet<Node>> graph, HashSet<Node> visitedNodes)
        {
            if (Equals(currentNode, targetNode))
            {
                _count++;

                return;
            }

            visitedNodes.Add(currentNode);

            var neighbors = graph[currentNode];

            foreach (Node neighbor in neighbors)
            {
                if (neighbor.IsBig || !visitedNodes.Contains(neighbor))
                {
                    if (neighbor.Name == "start")
                    {
                        continue;
                    }

                    ExplorePath(neighbor,
                                targetNode,
                                graph,
                                new HashSet<Node>(visitedNodes));
                }
            }
        }

        private void ExplorePath2(Node currentNode,
                                  Node targetNode,
                                  Dictionary<Node, HashSet<Node>> graph,
                                  HashSet<Node> visitedNodes,
                                  HashSet<Node> twiceVisistedSmallNodes)
        {
            if (Equals(currentNode, targetNode))
            {
                _count++;

                return;
            }

            bool added = visitedNodes.Add(currentNode);

            if (!added && !currentNode.IsBig)
            {
                twiceVisistedSmallNodes.Add(currentNode);
            }

            var neighbors = graph[currentNode];

            foreach (Node neighbor in neighbors)
            {
                if (neighbor.IsBig || !twiceVisistedSmallNodes.Contains(neighbor) && twiceVisistedSmallNodes.Count <= 1)
                {
                    if (neighbor.Name == "start")
                    {
                        continue;
                    }

                    ExplorePath2(neighbor,
                                 targetNode,
                                 graph,
                                 new HashSet<Node>(visitedNodes),
                                 new HashSet<Node>(twiceVisistedSmallNodes));
                }
            }
        }

        private struct Node
        {
            public Node(string name)
            {
                Name = name;
            }

            public override string ToString()
            {
                return Name;
            }

            public string Name { get; }
            public bool IsBig => Name.All(Char.IsUpper);
        }
    }

    [TestClass]
    public class Day12Tests
    {
        [TestMethod]
        public void Part1_Debug()
        {
            var sut = new Day12();
            int result = sut.Part1(Resources.Day12_Debug);
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void Part1()
        {
            var sut = new Day12();
            int result = sut.Part1(Resources.Day12);
            Assert.AreEqual(4411, result);
        }

        [TestMethod]
        public void Part2_Debug()
        {
            var sut = new Day12();
            int result = sut.Part2(Resources.Day12_Debug);
            Assert.AreEqual(36, result);
        }

        [TestMethod]
        public void Part2()
        {
            var sut = new Day12();
            int result = sut.Part2(Resources.Day12);
            Assert.AreEqual(136767, result);
        }
    }
}