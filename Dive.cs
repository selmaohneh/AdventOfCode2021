using AdventOfCode2021.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    internal class Dive
    {
        public int Part1(string input)
        {
            var commands = GetCommandsFromInput(input);

            int horizontalPosition = 0;
            int depth = 0;

            foreach (string command in commands)
            {
                (string commandAction, int commandArgument) = DeconstructCommand(command);

                if (commandAction == "forward")
                {
                    horizontalPosition += commandArgument;
                }

                if (commandAction == "up")
                {
                    depth -= commandArgument;
                }

                if (commandAction == "down")
                {
                    depth += commandArgument;
                }
            }

            return depth * horizontalPosition;
        }

        public int Part2(string input)
        {
            var commands = GetCommandsFromInput(input);

            int horizontalPosition = 0;
            int depth = 0;
            int aim = 0;

            foreach (string command in commands)
            {
                (string commandAction, int commandArgument) = DeconstructCommand(command);

                if (commandAction == "forward")
                {
                    horizontalPosition += commandArgument;
                    depth += commandArgument * aim;
                }

                if (commandAction == "up")
                {
                    aim -= commandArgument;
                }

                if (commandAction == "down")
                {
                    aim += commandArgument;
                }
            }

            return depth * horizontalPosition;
        }

        private List<string> GetCommandsFromInput(string input)
        {
            var commands = input.Split(Environment.NewLine).ToList();

            return commands;
        }

        private (string commandAction, int commandArgument) DeconstructCommand(string command)
        {
            string[] commandParts = command.Split(" ");
            string commandAction = commandParts[0];
            int commandArgument = Int32.Parse(commandParts[1]);

            return (commandAction, commandArgument);
        }
    }

    [TestClass]
    public class Day2
    {
        [TestMethod]
        public void Day2_Part1()
        {
            var sut = new Dive();

            int result = sut.Part1(Resources.Day2);

            Assert.AreEqual(1636725, result);
        }

        [TestMethod]
        public void Day2_Part2()
        {
            var sut = new Dive();

            int result = sut.Part2(Resources.Day2);

            Assert.AreEqual(1872757425, result);
        }
    }
}