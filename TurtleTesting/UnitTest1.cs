using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TurtleGame;

namespace TurtleTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(Factory.BuildPunter(10)  == null, true);
        }
    }
}
