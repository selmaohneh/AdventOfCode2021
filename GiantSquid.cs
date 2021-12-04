using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal class GiantSquid
    {
        public void Part1(string input)
        {
            var splitInput = input.Split(Environmennt.NewLine);
            var randomNumbers = splitInput[0];

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

            Assert.AreEqual(1636725, result);
        }
    }
}
