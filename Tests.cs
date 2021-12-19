﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    [TestClass]
    public class Tests
    {

       
       
           
           
      
      
               [TestMethod]
        public void Day15_Part1()
        {
            var sut = new Day15();
            string result = sut.Part1();
            Assert.AreEqual("390", result);
        }

        [TestMethod]
        public void Day15_Part2()
        {
            var sut = new Day15();
            string result = sut.Part2();
            Assert.AreEqual("2814", result);
        }
    }
}