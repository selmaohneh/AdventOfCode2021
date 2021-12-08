using AdventOfCode2021.Properties;

namespace AdventOfCode2021
{
    internal class Day8 : IAdeventOfCodeDay
    {
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

                var decodedSegments = DecodeAllSegments(inputValues);

                string numberString = String.Empty;

                foreach (string outputValue in outputValues)
                {
                    foreach (Segment segment in decodedSegments.Values)
                    {
                        if (segment.IsTheSameAs(outputValue))
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

        private Dictionary<int, Segment> DecodeAllSegments(string[] inputValues)
        {
            var decodedSegments = new Dictionary<int, Segment>();

            while (decodedSegments.Count < 10)
            {
                foreach (string inputValue in inputValues)
                {
                    int characterCount = inputValue.Length;

                    var rules = new List<bool>();

                    bool isAZero = decodedSegments.ContainsKey(1)
                                && decodedSegments.ContainsKey(4)
                                && decodedSegments[1].IsPresentIn(inputValue)
                                && !decodedSegments[4].IsPresentIn(inputValue)
                                && characterCount is 6;
                    bool isAOne = characterCount is 2;
                    bool isATwo = decodedSegments.ContainsKey(4) && decodedSegments[4].GetCommonWiresWith(inputValue) == 2 && characterCount is 5;
                    bool isAThree = decodedSegments.ContainsKey(1) && decodedSegments[1].IsPresentIn(inputValue) && characterCount is 5;
                    bool isAFour = characterCount is 4;
                    bool isAFive = decodedSegments.Count == 9 && decodedSegments.All(x => x.Value.Wires != inputValue);
                    bool isASix = decodedSegments.ContainsKey(1) && !decodedSegments[1].IsPresentIn(inputValue) && characterCount is 6;
                    bool isASeven = characterCount is 3;
                    bool isAnEight = characterCount is 7;
                    bool isANine = decodedSegments.ContainsKey(4) && decodedSegments[4].IsPresentIn(inputValue) && characterCount is 6;

                    rules.Add(isAZero);
                    rules.Add(isAOne);
                    rules.Add(isATwo);
                    rules.Add(isAThree);
                    rules.Add(isAFour);
                    rules.Add(isAFive);
                    rules.Add(isASix);
                    rules.Add(isASeven);
                    rules.Add(isAnEight);
                    rules.Add(isANine);

                    for (int number = 0; number < rules.Count; number++)
                    {
                        if (rules[number])
                        {
                            var segment = new Segment(number, inputValue);
                            decodedSegments.TryAdd(number, segment);
                        }
                    }
                }
            }

            return decodedSegments;
        }

        private class Segment
        {
            public Segment(int number, string wires)
            {
                Number = number;
                Wires = wires;
            }

            public int Number { get; }
            public string Wires { get; }

            public bool IsPresentIn(string inputValue)
            {
                char[] inputChars = inputValue.ToCharArray();

                foreach (char currentChar in Wires)
                {
                    if (!inputChars.Contains(currentChar))
                    {
                        return false;
                    }
                }

                return true;
            }

            public int GetCommonWiresWith(string inputValue)
            {
                int commonWires = 0;

                foreach (char currentChar in inputValue)
                {
                    if (Wires.Contains(currentChar))
                    {
                        commonWires++;
                    }
                }

                return commonWires;
            }

            public bool IsTheSameAs(string inputValue)
            {
                foreach (char currentChar in inputValue)
                {
                    if (!Wires.Contains(currentChar))
                    {
                        return false;
                    }
                }

                return inputValue.Length == Wires.Length;
            }
        }
    }
}