using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021
{
    [TestClass]
    public class Tests
    {

       
       
           
           
      
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