using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2021.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    internal class GiantSquid
    {
        public int Part1(string input)
        {
            string[] splitInput = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var randomNumbers = splitInput[0].Split(",").Select(Int32.Parse).ToList();

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

            foreach (int randomNumber in randomNumbers)
            {
                foreach (Board board in boards)
                {
                    board.Mark(randomNumber);
                    bool won = board.HasBoardWon();

                    if (won)
                    {
                        return board.GetSumOfUnmarkedNumbers() * randomNumber;
                    }
                }
            }

            throw new Exception();
        }
    }

    internal class Cell
    {
        public bool Marked { get; set; }
        public int Number { get; set; }
    }

    internal class Board
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

    [TestClass]
    public class Day4
    {
        [TestMethod]
        public void Day4_Part1()
        {
            var sut = new GiantSquid();

            int result = sut.Part1(Resources.Day4);

            Assert.AreEqual(23177, result);
        }
    }
}