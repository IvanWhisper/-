using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Demo;

namespace Demo.Tests
{
    [TestClass()]
    public class ProgramTest
    {
        [TestMethod()]
        public void plusTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddTest()
        {
           // Assert.IsTrue();
        }
    }
}

namespace TestDemo
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void AddTest()
        {
            int a = 100;
            int b = 10;
            Assert.AreEqual(Program.Add(a, b), 110);
        }
    }
}
