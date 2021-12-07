using AdventOfCode2021.Properties;

namespace AdventOfCode2021
{
    internal class Day2 : IAdeventOfCodeDay
    {
        public string Input { get; } = Resources.Day2;

        public string Part1()
        {
            var commands = GetCommandsFromInput(Input);

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

            return (depth * horizontalPosition).ToString();
        }

        public string Part2()
        {
            var commands = GetCommandsFromInput(Input);

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

            return (depth * horizontalPosition).ToString();
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
}