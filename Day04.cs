using AdventOfCode2021.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    internal class Day04
    {
        public int Part1(string input)
        {
            string[] splitInput = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            List<int> randomNumbers = ExtractRandomNumbers(splitInput);

            List<Board> boards = BuildsBoards(splitInput);

            foreach (int randomNumber in randomNumbers)
            {
                foreach (Board board in boards)
                {
                    board.Mark(randomNumber);
                    bool won = board.HasBoardWon();

                    if (won)
                    {
                        return (board.GetSumOfUnmarkedNumbers() * randomNumber);
                    }
                }
            }

            throw new Exception();
        }

        public int Part2(string input)
        {
            string[] splitInput = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            List<int> randomNumbers = ExtractRandomNumbers(splitInput);

            List<Board> boards = BuildsBoards(splitInput);

            foreach (int randomNumber in randomNumbers)
            {
                foreach (Board board in boards)
                {
                    board.Mark(randomNumber);
                    board.HasBoardWon();

                    if (boards.All(x => x.HasBoardWon()))
                    {
                        return (board.GetSumOfUnmarkedNumbers() * randomNumber);
                    }
                }
            }

            throw new Exception();
        }

        private List<int> ExtractRandomNumbers(string[] splitInput)
        {
            var randomNumbers = splitInput[0].Split(",").Select(Int32.Parse).ToList();

            return randomNumbers;
        }

        private List<Board> BuildsBoards(string[] splitInput)
        {
            var boards = new List<Board>();
            var currentBoard = new Board();

            for (int i = 1; i < splitInput.Length; i++)
            {
                if (currentBoard.IsFull)
                {
                    boards.Add(currentBoard);
                    currentBoard = new Board();
                }

                var numbersOfLine = splitInput[i].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToList();

                foreach (int numberOfLine in numbersOfLine)
                {
                    var cell = new Cell
                    {
                        Number = numberOfLine
                    };
                    currentBoard.Cells.Add(cell);
                }
            }

            boards.Add(currentBoard);

            return boards;
        }

        private class Cell
        {
            public bool Marked { get; set; }
            public int Number { get; set; }
        }

        private class Board
        {
            public List<Cell> Cells { get; set; } = new();
            public bool IsFull => Cells.Count >= 25;

            public void Mark(int number)
            {
                foreach (Cell cell in Cells)
                {
                    if (cell.Number == number)
                    {
                        cell.Marked = true;
                    }
                }
            }

            public bool HasBoardWon()
            {
                return IsAnyRowComplete() || IsAnyColumnComplete() || IsDiagonalComplete();
            }

            public bool IsAnyRowComplete()
            {
                for (int i = 0; i < 5; i++)
                {
                    var row = Cells.Skip(i * 5).Take(5);

                    if (row.All(x => x.Marked))
                    {
                        return true;
                    }
                }

                return false;
            }

            public bool IsAnyColumnComplete()
            {
                for (int i = 0; i < 5; i++)
                {
                    var column = new List<Cell>();

                    for (int k = 0; k < 5; k++)
                    {
                        column.Add(Cells[i + k * 5]);
                    }

                    if (column.All(x => x.Marked))
                    {
                        return true;
                    }
                }

                return false;
            }

            public bool IsDiagonalComplete()
            {
                var diagonal = new List<Cell>();

                for (int i = 0; i < 5; i++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        if (i == k)
                        {
                            diagonal.Add(Cells[i * 5 + k]);
                        }
                    }
                }

                if (diagonal.All(x => x.Marked))
                {
                    return true;
                }

                return false;
            }

            public int GetSumOfUnmarkedNumbers()
            {
                return Cells.Where(x => !x.Marked).Select(x => x.Number).Sum();
            }
        }
    }

    [TestClass]
    public class Day04Tests
    {
        [TestMethod]
        public void Part1_Debug()
        {
            var sut = new Day04();
            int result = sut.Part1(Resources.Day04_Debug);
            Assert.AreEqual(4512, result);
        }

        [TestMethod]
        public void Part1()
        {
            var sut = new Day04();
            int result = sut.Part1(Resources.Day04);
            Assert.AreEqual(23177, result);
        }

        [TestMethod]
        public void Part2_Debug()
        {
            var sut = new Day04();
            int result = sut.Part2(Resources.Day04_Debug);
            Assert.AreEqual(1924, result);
        }

        [TestMethod]
        public void Day4_Part2()
        {
            var sut = new Day04();
            int result = sut.Part2(Resources.Day04);
            Assert.AreEqual(6804, result);
        }
    }
}