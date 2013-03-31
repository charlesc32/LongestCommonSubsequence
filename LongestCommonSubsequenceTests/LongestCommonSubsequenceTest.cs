using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LongestCommonSubsequenceTests
{
    [TestClass]
    public class LongestCommonSubsequenceTests
    {
        [TestMethod]
        public void FindMostCommonSubSequenceTest()
        {
            string actual = Program.FindMostCommonSubSequence("XMJYAUZ", "MZJAWXU");
            string expected = "MJAU";
            Assert.AreEqual(expected, actual);

            actual = Program.FindMostCommonSubSequence("CHARLES", "CHARLESCYMBOR");
            expected = "CHARLES";
            Assert.AreEqual(expected, actual);

            actual = Program.FindMostCommonSubSequence("CYMBORCHARLES", "CHARLESCYMBOR");
            expected = "CHARLES";
            Assert.AreEqual(expected, actual);
            
            actual = Program.FindMostCommonSubSequence("the quick brown fox", "the fast brown dogs");
            expected = "the  brown o";
            Assert.AreEqual(expected, actual);
            
            actual = Program.FindMostCommonSubSequence("hello world mordor", "lord of the rings");
            expected = "lord or";
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void MainDriverTest()
        {
            var testInput = new Dictionary<int, string>() {{1, "XMJYAUZ;MZJAWXU"}, {2, "CHARLES;CHARLESCYMBOR"}, {3,"CYMBORCHARLES;CHARLESCYMBOR"}};
            var actual = Program.MainDriver(testInput);
            var expected = new List<string>() {"MJAU", "CHARLES", "CHARLES"};
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
