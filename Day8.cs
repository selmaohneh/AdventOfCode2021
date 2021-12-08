using AdventOfCode2021.Properties;

namespace AdventOfCode2021
{
    internal class Day8 : IAdeventOfCodeDay
    {
        private Dictionary<int, Segment> _decodedSegments;
        public string Input => Resources.Day8;

        public string Part1()
        {
            string[] lines = Input.Split(Environment.NewLine);

            int result = 0;

            foreach (string line in lines)
            {
                string[] splitValues = line.Split(" | ");
                string[] outputValues = splitValues[1].Split(" ");

                foreach (string outputValue in outputValues)
                {
                    int characterCount = outputValue.Length;

                    if (characterCount is 2 or 4 or 3 or 7)
                    {
                        result++;
                    }
                }
            }

            return result.ToString();
        }

        public string Part2()
        {
            string[] lines = Input.Split(Environment.NewLine);

            int result = 0;

            foreach (string line in lines)
            {
                string[] splitValues = line.Split(" | ");
                string[] inputValues = splitValues[0].Split(" ");
                string[] outputValues = splitValues[1].Split(" ");

                _decodedSegments = new Dictionary<int, Segment>();

                while (_decodedSegments.Count < 10)
                {
                    foreach (string inputValue in inputValues)
                    {
                        int characterCount = inputValue.Length;

                        if (characterCount is 2)
                        {
                            AddToDecodedSegments(1, inputValue);
                        }

                        if (characterCount is 4)
                        {
                            AddToDecodedSegments(4, inputValue);
                        }

                        if (characterCount is 3)
                        {
                            AddToDecodedSegments(7, inputValue);
                        }

                        if (characterCount is 7)
                        {
                            AddToDecodedSegments(8, inputValue);
                        }

                        if (_decodedSegments.ContainsKey(4) && _decodedSegments[4].ContainsAllWiresFrom(inputValue) && characterCount is 6)
                        {
                            AddToDecodedSegments(9, inputValue);
                        }

                        if (_decodedSegments.ContainsKey(1) && !_decodedSegments[1].ContainsAllWiresFrom(inputValue) && characterCount is 6)
                        {
                            AddToDecodedSegments(6, inputValue);
                        }

                        if (_decodedSegments.ContainsKey(1) && _decodedSegments[1].ContainsAllWiresFrom(inputValue) && characterCount is 5)
                        {
                            AddToDecodedSegments(3, inputValue);
                        }

                        if (_decodedSegments.ContainsKey(1)
                         && _decodedSegments.ContainsKey(4)
                         && _decodedSegments[1].ContainsAllWiresFrom(inputValue)
                         && !_decodedSegments[4].ContainsAllWiresFrom(inputValue)
                         && characterCount is 6)
                        {
                            AddToDecodedSegments(0, inputValue);
                        }

                        if (_decodedSegments.ContainsKey(4) && _decodedSegments[4].GetCommonWiresWith(inputValue) == 2 && characterCount is 5)
                        {
                            AddToDecodedSegments(2, inputValue);
                        }

                        if (_decodedSegments.Count == 9)
                        {
                            if (_decodedSegments.Any(x => x.Value.Equal(inputValue)))
                            {
                                continue;
                            }

                            AddToDecodedSegments(5, inputValue);
                        }
                    }
                }

                string numberString = String.Empty;

                foreach (string outputValue in outputValues)
                {
                    foreach (Segment segment in _decodedSegments.Values)
                    {
                        var bla = segment.Chars.ToList();

                        if (segment.Equal(outputValue))
                        {
                            numberString += segment.Number.ToString();
                        }
                    }
                }

                int number = Int32.Parse(numberString);
                result += number;
            }

            return result.ToString();
        }

        private void AddToDecodedSegments(int number, string inputValue)
        {
            var segment = new Segment
            {
                Number = number,
                Chars = inputValue.ToCharArray()
            };

            _decodedSegments.TryAdd(number, segment);
        }

        private class Segment
        {
            public int Number { get; set; }
            public IEnumerable<char> Chars { get; set; }

            public bool ContainsAllWiresFrom(string inputValue)
            {
                var inputChars = inputValue.ToCharArray();

                foreach (char currentChar in Chars)
                {
                    if (!inputChars.Contains(currentChar))
                    {
                        return false;
                    }
                }

                return true;
            }

            public bool Equal(string inputValue)
            {
                foreach (char currentChar in inputValue)
                {
                    if (!Chars.Contains(currentChar))
                    {
                        return false;
                    }
                }

                return inputValue.Length == Chars.Count();
            }

            public int GetCommonWiresWith(string inputValue)
            {
                int commonWires = 0;

                foreach (char currentChar in inputValue.ToCharArray())
                {
                    if (Chars.Contains(currentChar))
                    {
                        commonWires++;
                    }
                }

                return commonWires;
            }
        }
    }
}