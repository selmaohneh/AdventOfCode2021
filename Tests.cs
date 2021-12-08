﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Day1_Part1()
        {
            var sut = new Day1();
            string result = sut.Part1();
            Assert.AreEqual("1301", result);
        }

        [TestMethod]
        public void Day1_Part2()
        {
            var sut = new Day1();
            string result = sut.Part2();
            Assert.AreEqual("1346", result);
        }

        [TestMethod]
        public void Day2_Part1()
        {
            var sut = new Day2();
            string result = sut.Part1();
            Assert.AreEqual("1636725", result);
        }

        [TestMethod]
        public void Day2_Part2()
        {
            var sut = new Day2();
            string result = sut.Part2();
            Assert.AreEqual("1872757425", result);
        }

        [TestMethod]
        public void Day3_Part1()
        {
            var sut = new Day3();
            string result = sut.Part1();
            Assert.AreEqual("841526", result);
        }

        [TestMethod]
        public void Day3_Part2()
        {
            var sut = new Day3();
            string result = sut.Part2();
            Assert.AreEqual("4790390", result);
        }

        [TestMethod]
        public void Day4_Part1()
        {
            var sut = new Day4();
            string result = sut.Part1();
            Assert.AreEqual("23177", result);
        }

        [TestMethod]
        public void Day4_Part2()
        {
            var sut = new Day4();
            string result = sut.Part2();
            Assert.AreEqual("6804", result);
        }

        [TestMethod]
        public void Day5_Part1()
        {
            var sut = new Day5();
            string result = sut.Part1();
            Assert.AreEqual("6397", result);
        }

        [TestMethod]
        public void Day5_Part2()
        {
            var sut = new Day5();
            string result = sut.Part2();
            Assert.AreEqual("22335", result);
        }

        [TestMethod]
        public void Day6_Part1()
        {
            var sut = new Day6();
            string result = sut.Part1();
            Assert.AreEqual("372300", result);
        }

        [TestMethod]
        public void Day6_Part2()
        {
            var sut = new Day6();
            string result = sut.Part2();
            Assert.AreEqual("1675781200288", result);
        }

        [TestMethod]
        public void Day7_Part1()
        {
            var sut = new Day7();
            string result = sut.Part1();
            Assert.AreEqual("328262", result);
        }

        [TestMethod]
        public void Day7_Part2()
        {
            var sut = new Day7();
            string result = sut.Part2();
            Assert.AreEqual("90040997", result);
        }

        [TestMethod]
        public void Day8_Part1()
        {
            var sut = new Day8();
            string result = sut.Part1();
            Assert.AreEqual("294", result);
        }

        [TestMethod]
        public void Day8_Part2()
        {
            var sut = new Day8();
            string result = sut.Part2();
            Assert.AreEqual("973292", result);
        }
    }
}