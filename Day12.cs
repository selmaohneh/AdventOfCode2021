using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2021.Properties;

namespace AdventOfCode2021
{
    internal class Day12 : IAdeventOfCodeDay
    {
        public string Input => Resources.Day12;

        public string Part1()
        {
            string[] lines = Input.Split(Environment.NewLine);

            var nodes = new Dictionary<string, Node>();
            var edges = new HashSet<Edge>();

            foreach (string line in lines)
            {
                string[] splitLine = line.Split("-");

                var nodeA = new Node(splitLine[0]);
                nodes.TryAdd(nodeA.Name, nodeA);

                var nodeB = new Node(splitLine[1]);
                nodes.TryAdd(nodeB.Name, nodeB);

                var edge = new Edge(nodeA, nodeB);
                edges.Add(edge);
            }

            ExplorePath(new Node("start"),
                        new Node("end"),
                        edges,
                        new HashSet<Node>());

            return _count.ToString();
        }

        public string Part2()
        {
            throw new NotImplementedException();
        }

        private int _count = 0;

        private void ExplorePath(Node currentNode, Node targetNode, HashSet<Edge> edges, HashSet<Node> visitedNodes)
        {
            if (Equals(currentNode, targetNode))
            {
                _count++;

                return;
            }

            visitedNodes.Add(currentNode);

            var matchingEdges = edges.Where(x => Equals(x.A, currentNode) || Equals(x.B, currentNode));

            foreach (Edge edge in matchingEdges)
            {
                Node neighbor;

                if (Equals(edge.A, currentNode))
                {
                    neighbor = edge.B;
                }
                else
                {
                    neighbor = edge.A;
                }

                if (neighbor.IsBig || !visitedNodes.Contains(neighbor))
                {
                    ExplorePath(neighbor,
                                targetNode,
                                edges,
                                new HashSet<Node>(visitedNodes));
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

        private struct Edge
        {
            public Edge(Node a, Node b)
            {
                A = a;
                B = b;
            }

            public Node A { get; }
            public Node B { get; }

            public override string ToString()
            {
                return $"{A} --> {B}";
            }
        }
    }
}