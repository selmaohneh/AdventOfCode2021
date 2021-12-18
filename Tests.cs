using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    [TestClass]
    public class Tests
    {

       
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

        [TestMethod]
        public void Day9_Part1()
        {
            var sut = new Day9();
            string result = sut.Part1();
            Assert.AreEqual("577", result);
        }

        [TestMethod]
        public void Day9_Part2()
        {
            var sut = new Day9();
            string result = sut.Part2();
            Assert.AreEqual("1069200", result);
        }

        [TestMethod]
        public void Day10_Part1()
        {
            var sut = new Day10();
            string result = sut.Part1();
            Assert.AreEqual("387363", result);
        }

        [TestMethod]
        public void Day10_Part2()
        {
            var sut = new Day10();
            string result = sut.Part2();
            Assert.AreEqual("4330777059", result);
        }

        [TestMethod]
        public void Day11_Part1()
        {
            var sut = new Day11();
            string result = sut.Part1();
            Assert.AreEqual("1732", result);
        }

        [TestMethod]
        public void Day11_Part2()
        {
            var sut = new Day11();
            string result = sut.Part2();
            Assert.AreEqual("290", result);
        }

        [TestMethod]
        public void Day12_Part1()
        {
            var sut = new Day12();
            string result = sut.Part1();
            Assert.AreEqual("4411", result);
        }

        [TestMethod]
        public void Day12_Part2()
        {
            var sut = new Day12();
            string result = sut.Part2();
            Assert.AreEqual("136767", result);
        }

        [TestMethod]
        public void Day13_Part1()
        {
            var sut = new Day13();
            string result = sut.Part1();
            Assert.AreEqual("671", result);
        }

        [TestMethod]
        public void Day13_Part2()
        {
            var sut = new Day13();
            string result = sut.Part2();

            Assert.AreEqual(@"###...##..###..#..#..##..###..#..#.#...
#..#.#..#.#..#.#..#.#..#.#..#.#.#..#...
#..#.#....#..#.####.#..#.#..#.##...#...
###..#....###..#..#.####.###..#.#..#...
#....#..#.#....#..#.#..#.#.#..#.#..#...
#.....##..#....#..#.#..#.#..#.#..#.####
",
                            result);
        }

        [TestMethod]
        public void Day14_Part1()
        {
            var sut = new Day14();
            string result = sut.Part1();
            Assert.AreEqual("3143", result);
        }

        [TestMethod]
        public void Day14_Part2()
        {
            var sut = new Day14();
            string result = sut.Part2();
            Assert.AreEqual("4110215602456", result);
        }

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