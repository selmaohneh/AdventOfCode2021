using AdventOfCode2021.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    internal class Day14
    {
        private Dictionary<string, long> _characterCounts;

        public int Part1(string input)
        {
            (string template, List<InsertionRule> rules) = GetTemplateAndInsertionRules(input);

            string result = template;

            for (int step = 0; step < 10; step++)
            {
                int offset = 0;

                for (int i = 0; i < template.Length - 1; i++)
                {
                    string currentPair = new(template.Skip(i).Take(2).ToArray());

                    InsertionRule? rule = rules.SingleOrDefault(x => x.Pair == currentPair);

                    if (rule == null)
                    {
                        continue;
                    }

                    result = result.Insert(i + 1 + offset, rule.Insertion);
                    offset++;
                }

                template = result;
            }

            var groups = result.GroupBy(x => x).OrderByDescending(x => x.Count());
            int mostCommon = groups.First().Count();
            int leastCommon = groups.Last().Count();

            return (mostCommon - leastCommon);
        }

        private (string template, List<InsertionRule> rules) GetTemplateAndInsertionRules(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            string template = lines[0];

            var rules = new List<InsertionRule>();

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] splitLine = line.Split(" -> ");

                var rule = new InsertionRule(splitLine[0], splitLine[1]);
                rules.Add(rule);
            }

            return (template, rules);
        }

        public long Part2(string input)
        {
            (string template, List<InsertionRule> rules) = GetTemplateAndInsertionRules(input);

            // count initial amount of rules in template.
            for (int i = 0; i < template.Length - 1; i++)
            {
                string currentPair = new(template.Skip(i).Take(2).ToArray());

                InsertionRule rule = rules.Single(x => x.Pair == currentPair);
                rule.Count++;
            }

            // count initial amounts of each character in template. 
            _characterCounts = new Dictionary<string, long>();

            foreach (char character in template)
            {
                AddOrIncrement(character.ToString(), 1);
            }

            // the actual steps
            for (int step = 0; step < 40; step++)
            {
                foreach (InsertionRule insertionRule in rules)
                {
                    // the rule can't be applied in this step since that pair is not present.
                    if (insertionRule.Count <= 0)
                    {
                        continue;
                    }

                    // since we inject a char in between each pair, all pairs will break and their count decreases.
                    insertionRule.IncrementForNextStep -= insertionRule.Count;

                    // update the counts per rule with the new pairs that spawned due to the injection.
                    string spawnedPairLeft = $"{insertionRule.Pair[0]}{insertionRule.Insertion}";
                    InsertionRule leftRule = rules.Single(x => x.Pair == spawnedPairLeft);
                    leftRule.IncrementForNextStep += insertionRule.Count;

                    string spawnedPairRight = $"{insertionRule.Insertion}{insertionRule.Pair[1]}";
                    InsertionRule rightRule = rules.Single(x => x.Pair == spawnedPairRight);
                    rightRule.IncrementForNextStep += insertionRule.Count;

                    // add the spawned chars to the count
                    AddOrIncrement(insertionRule.Insertion, insertionRule.Count);
                }

                foreach (InsertionRule rule in rules)
                {
                    // the step is over - we can now increment the rule counts
                    rule.Count += rule.IncrementForNextStep;
                    rule.IncrementForNextStep = 0;
                }
            }

            long mostCommon = _characterCounts.Select(x => x.Value).Max();
            long leastCommon = _characterCounts.Select(x => x.Value).Min();

            return (mostCommon - leastCommon);
        }

        private void AddOrIncrement(string character, long count)
        {
            bool isNew = _characterCounts.TryAdd(character, count);

            if (!isNew)
            {
                _characterCounts[character] += count;
            }
        }

        private class InsertionRule
        {
            public InsertionRule(string pair, string insertion)
            {
                Pair = pair;
                Insertion = insertion;
            }

            public string Pair { get; }
            public string Insertion { get; }
            public long Count { get; set; }

            public long IncrementForNextStep { get; set; }

            public override string ToString()
            {
                return $"{Pair} --> {Insertion}: {Count}";
            }
        }
    }

    [TestClass]
    public class Day14Tests
    {
        [TestMethod]
        public void Part1_Debug()
        {
            var sut = new Day14();
            int result = sut.Part1(Resources.Day14_Debug);
            Assert.AreEqual(1588, result);
        }

        [TestMethod]
        public void Part1()
        {
            var sut = new Day14();
            int result = sut.Part1(Resources.Day14);
            Assert.AreEqual(3143, result);
        }

        [TestMethod]
        public void Part2_Debug()
        {
            var sut = new Day14();
            long result = sut.Part2(Resources.Day14_Debug);
            Assert.AreEqual(2188189693529, result);
        }

        [TestMethod]
        public void Part2()
        {
            var sut = new Day14();
            long result = sut.Part2(Resources.Day14);
            Assert.AreEqual(4110215602456, result);
        }
    }
}