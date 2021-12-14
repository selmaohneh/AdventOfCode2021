﻿using AdventOfCode2021.Properties;

namespace AdventOfCode2021
{
    internal class Day14 : IAdeventOfCodeDay
    {
        private Dictionary<string, long> _characterCounts;
        public string Input => Resources.Day14;

        public string Part1()
        {
            (string template, List<InsertionRule> rules) = GetTemplateAndInsertionRules();

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

            return (mostCommon - leastCommon).ToString();
        }

        private (string template, List<InsertionRule> rules) GetTemplateAndInsertionRules()
        {
            string[] lines = Input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

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

        public string Part2()
        {
            (string template, List<InsertionRule> rules) = GetTemplateAndInsertionRules();

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

            return (mostCommon - leastCommon).ToString();
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
}