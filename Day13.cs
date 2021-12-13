using AdventOfCode2021.Properties;

namespace AdventOfCode2021
{
    internal class Day13 : IAdeventOfCodeDay
    {
        public string Input => Resources.Day13;

        public string Part1()
        {
            (List<FoldInstruction> foldInstructions, List<Cell> dots) = GetDotsAndFoldInstructions();

            var dotsAfterFold = new List<Cell>();

            foreach (FoldInstruction foldInstruction in foldInstructions)
            {
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

                // only the first fold
                break;
            }

            int count = dotsAfterFold.GroupBy(p => new
                                      {
                                          p.X,
                                          p.Y
                                      })
                                     .Count();

            return count.ToString();
        }

        private (List<FoldInstruction> foldInstructions, List<Cell> dots) GetDotsAndFoldInstructions()
        {
            string[] lines = Input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

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

        public string Part2()
        {
            (List<FoldInstruction> foldInstructions, List<Cell> dots) = GetDotsAndFoldInstructions();

            List<Cell> dotsAfterFold;

            foreach (FoldInstruction foldInstruction in foldInstructions)
            {
                dotsAfterFold = new List<Cell>();

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
}