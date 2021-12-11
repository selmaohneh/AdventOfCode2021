using AdventOfCode2021.Properties;

namespace AdventOfCode2021
{
    internal class Day11 : IAdeventOfCodeDay
    {
        public string Input => Resources.Day11;

        public string Part1()
        {
            Board board = CreateBoard();

            for (int step = 0; step < 100; step++)
            {
                board.NextStep();
            }

            return board.Flashes.ToString();
        }

        public string Part2()
        {
            Board board = CreateBoard();

            int step = 0;

            while (true)
            {
                step++;

                board.NextStep();

                if (board.Cells.All(x => x.FlashedThisStep))
                {
                    return step.ToString();
                }
            }
        }

        private Board CreateBoard()
        {
            string[] lines = Input.Split(Environment.NewLine);

            var cells = new List<Cell>();

            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];
                var energyLevels = line.ToCharArray().Select(x => Int64.Parse(x.ToString())).ToList();

                for (int x = 0; x < energyLevels.Count; x++)
                {
                    var cell = new Cell(x, y, energyLevels[x]);
                    cells.Add(cell);
                }
            }

            var board = new Board
            {
                Cells = cells
            };

            return board;
        }

        private class Cell
        {
            public Cell(int x, int y, long energyLevel)
            {
                X = x;
                Y = y;
                EnergyLevel = energyLevel;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public long EnergyLevel { get; set; }
            public bool FlashedThisStep { get; set; }
        }

        private class Board
        {
            public List<Cell> Cells { get; set; } = new();

            public int Flashes { get; set; }

            public void NextStep()
            {
                foreach (Cell cell in Cells)
                {
                    cell.FlashedThisStep = false;
                }

                var queue = new Queue<Cell>(Cells);

                while (queue.Any())
                {
                    Cell cell = queue.Dequeue();
                    cell.EnergyLevel++;

                    if (cell.EnergyLevel > 9 && !cell.FlashedThisStep)
                    {
                        cell.FlashedThisStep = true;
                        Flashes++;
                        var neighbours = GetNeighbours(cell);

                        foreach (Cell neighbour in neighbours)
                        {
                            queue.Enqueue(neighbour);
                        }
                    }
                }

                foreach (Cell cell in Cells)
                {
                    if (cell.EnergyLevel > 9)
                    {
                        cell.EnergyLevel = 0;
                    }
                }
            }

            private IEnumerable<Cell> GetNeighbours(Cell cell)
            {
                var neighbours = new List<Cell>();

                if (cell.X < 9)
                {
                    Cell rightNeighbour = Cells.Single(x => x.X == cell.X + 1 && x.Y == cell.Y);
                    neighbours.Add(rightNeighbour);
                }

                if (cell.X > 0)
                {
                    Cell leftNeighbour = Cells.Single(x => x.X == cell.X - 1 && x.Y == cell.Y);
                    neighbours.Add(leftNeighbour);
                }

                if (cell.Y > 0)
                {
                    Cell topNeighbour = Cells.Single(x => x.X == cell.X && x.Y == cell.Y - 1);
                    neighbours.Add(topNeighbour);
                }

                if (cell.Y < 9)
                {
                    Cell bottomNeighbour = Cells.Single(x => x.X == cell.X && x.Y == cell.Y + 1);
                    neighbours.Add(bottomNeighbour);
                }

                if (cell.X > 0 && cell.Y > 0)
                {
                    Cell topLeftNeighbour = Cells.Single(x => x.X == cell.X - 1 && x.Y == cell.Y - 1);
                    neighbours.Add(topLeftNeighbour);
                }

                if (cell.X < 9 && cell.Y > 0)
                {
                    Cell topRightNeighbour = Cells.Single(x => x.X == cell.X + 1 && x.Y == cell.Y - 1);
                    neighbours.Add(topRightNeighbour);
                }

                if (cell.X > 0 && cell.Y < 9)
                {
                    Cell bottomLeftNeighbour = Cells.Single(x => x.X == cell.X - 1 && x.Y == cell.Y + 1);
                    neighbours.Add(bottomLeftNeighbour);
                }

                if (cell.X < 9 && cell.Y < 9)
                {
                    Cell bottomRightNeighbour = Cells.Single(x => x.X == cell.X + 1 && x.Y == cell.Y + 1);
                    neighbours.Add(bottomRightNeighbour);
                }

                return neighbours;
            }
        }
    }
}