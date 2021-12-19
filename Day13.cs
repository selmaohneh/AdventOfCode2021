using AdventOfCode2021.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    internal class Day13
    {
        public int Part1(string input)
        {
            (List<FoldInstruction> foldInstructions, List<Cell> dots) = GetDotsAndFoldInstructions(input);

            dots = Fold(foldInstructions.Take(1).ToList(), dots);

            int count = dots.GroupBy(p => new
                             {
                                 p.X,
                                 p.Y
                             })
                            .Count();

            return count;
        }

        private (List<FoldInstruction> foldInstructions, List<Cell> dots) GetDotsAndFoldInstructions(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var foldInstructions = new List<FoldInstruction>();
            var dots = new List<Cell>();

            foreach (string line in lines)
            {
                if (line.StartsWith("fold"))
                {
                    string[] instruction = line.Split("fold along ");
                    string[] instructionParts = instruction[1].Split("=");
                    string coordinate = instructionParts[0];
                    int number = Int32.Parse(instructionParts[1]);

                    var foldInstruction = new FoldInstruction(coordinate, number);
                    foldInstructions.Add(foldInstruction);

                    continue;
                }

                string[] dotCoordinates = line.Split(',');
                int x = Int32.Parse(dotCoordinates[0]);
                int y = Int32.Parse(dotCoordinates[1]);

                var cell = new Cell(x, y);
                dots.Add(cell);
            }

            return (foldInstructions, dots);
        }

        public string Part2(string input)
        {
            (List<FoldInstruction> foldInstructions, List<Cell> dots) = GetDotsAndFoldInstructions(input);

            dots = Fold(foldInstructions, dots);

            int maxX = dots.Max(x => x.X);
            int maxY = dots.Max(x => x.Y);

            string plot = String.Empty;

            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    if (dots.Any(d => d.X == x && d.Y == y))
                    {
                        plot += "#";
                    }
                    else
                    {
                        plot += ".";
                    }
                }

                plot += Environment.NewLine;
            }

            return plot;
        }

        private List<Cell> Fold(List<FoldInstruction> foldInstructions, List<Cell> dots)
        {
            foreach (FoldInstruction foldInstruction in foldInstructions)
            {
                var dotsAfterFold = new List<Cell>();

                if (foldInstruction.Coordinate == "x")
                {
                    dotsAfterFold.AddRange(dots.Where(x => x.X < foldInstruction.Number));

                    var dotsRightOfLine = dots.Where(x => x.X > foldInstruction.Number);

                    foreach (Cell cell in dotsRightOfLine)
                    {
                        int distanceToLine = cell.X - foldInstruction.Number;
                        int newX = cell.X - 2 * distanceToLine;

                        if (newX >= 0)
                        {
                            cell.X = newX;
                            dotsAfterFold.Add(cell);
                        }
                    }
                }

                if (foldInstruction.Coordinate == "y")
                {
                    dotsAfterFold.AddRange(dots.Where(x => x.Y < foldInstruction.Number));

                    var dotsBelowLine = dots.Where(x => x.Y > foldInstruction.Number);

                    foreach (Cell cell in dotsBelowLine)
                    {
                        int distanceToLine = cell.Y - foldInstruction.Number;
                        int newY = cell.Y - 2 * distanceToLine;

                        if (newY >= 0)
                        {
                            cell.Y = newY;
                            dotsAfterFold.Add(cell);
                        }
                    }
                }

                dots = dotsAfterFold;
            }

            return dots;
        }

        private class Cell
        {
            public Cell(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }
        }

        private class FoldInstruction
        {
            public FoldInstruction(string coordinate, int number)
            {
                Coordinate = coordinate;
                Number = number;
            }

            public string Coordinate { get; set; }
            public int Number { get; set; }
        }
    }

    [TestClass]
    public class Day13Tests
    {
        [TestMethod]
        public void Part1_Debug()
        {
            var sut = new Day13();
            int result = sut.Part1(Resources.Day13_Debug);
            Assert.AreEqual(17, result);
        }

        [TestMethod]
        public void Part1()
        {
            var sut = new Day13();
            int result = sut.Part1(Resources.Day13);
            Assert.AreEqual(671, result);
        }

        [TestMethod]
        public void Part2()
        {
            var sut = new Day13();
            string result = sut.Part2(Resources.Day13);

            Assert.AreEqual(@"###...##..###..#..#..##..###..#..#.#...
#..#.#..#.#..#.#..#.#..#.#..#.#.#..#...
#..#.#....#..#.####.#..#.#..#.##...#...
###..#....###..#..#.####.###..#.#..#...
#....#..#.#....#..#.#..#.#.#..#.#..#...
#.....##..#....#..#.#..#.#..#.#..#.####
",
                            result);
        }
    }
}