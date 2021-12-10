using System.Globalization;
using AdventOfCode2021.Properties;

namespace AdventOfCode2021
{
    internal class Day10 : IAdeventOfCodeDay
    {
        private Stack<char> _openingBrackets;
        private int _points;
        public string Input => Resources.Day10;

        public string Part1()
        {
            string[] lines = Input.Split(Environment.NewLine);

            _points = 0;

            foreach (string line in lines)
            {
                (bool valid, char lastChar) stack = BuildValidStack(line);

                if (!stack.valid)
                {
                    AddPointsIfApplicable(stack.lastChar);
                }
            }

            return _points.ToString();
        }

        private bool PushIfOpeningBracket(char currentChar, Stack<char> openingBrackets)
        {
            if (currentChar is '(' or '[' or '{' or '<')
            {
                openingBrackets.Push(currentChar);

                return true;
            }

            return false;
        }

        private bool PopIfValidClosingBracket(char currentChar, char openingChar, char closingChar)
        {
            if (currentChar == closingChar && _openingBrackets.Peek() == openingChar)
            {
                _openingBrackets.Pop();

                return true;
            }

            return false;
        }

        private void AddPointsIfApplicable(char currentChar)
        {
            if (currentChar is ')')
            {
                _points += 3;
            }

            if (currentChar is ']')
            {
                _points += 57;
            }

            if (currentChar is '}')
            {
                _points += 1197;
            }

            if (currentChar is '>')
            {
                _points += 25137;
            }
        }

        public string Part2()
        {
            string[] lines = Input.Split(Environment.NewLine);

            var pointsPerLine = new List<double>();

            foreach (string line in lines)
            {
                (bool valid, char lastChar) stack = BuildValidStack(line);

                if (!stack.valid)
                {
                    continue;
                }

                double points = 0;

                while (_openingBrackets.Any())
                {
                    char lastOpeningBracket = _openingBrackets.Pop();
                    points *= 5;

                    if (lastOpeningBracket == '(')
                    {
                        points += 1;
                    }

                    if (lastOpeningBracket == '[')
                    {
                        points += 2;
                    }

                    if (lastOpeningBracket == '{')
                    {
                        points += 3;
                    }

                    if (lastOpeningBracket == '<')
                    {
                        points += 4;
                    }
                }

                pointsPerLine.Add(points);
            }

            int middleIndex = pointsPerLine.Count / 2;
            var orderedPoints = pointsPerLine.OrderByDescending(x => x);

            return orderedPoints.ElementAt(middleIndex).ToString(CultureInfo.InvariantCulture);
        }

        private (bool valid, char lastChar) BuildValidStack(string line)
        {
            _openingBrackets = new Stack<char>();
            var chars = line.ToCharArray().ToList();

            foreach (char currentChar in chars)
            {
                bool charValid = PushIfOpeningBracket(currentChar, _openingBrackets);

                charValid |= PopIfValidClosingBracket(currentChar,
                                                      '(',
                                                      ')');

                charValid |= PopIfValidClosingBracket(currentChar,
                                                      '[',
                                                      ']');

                charValid |= PopIfValidClosingBracket(currentChar,
                                                      '{',
                                                      '}');

                charValid |= PopIfValidClosingBracket(currentChar,
                                                      '<',
                                                      '>');

                if (!charValid)
                {
                    return (false, currentChar);
                }
            }

            // yak - of course we should extract this into an own class, but this is Aoc --> ain't no body got time for that.
            return (true, 'x');
        }
    }
}