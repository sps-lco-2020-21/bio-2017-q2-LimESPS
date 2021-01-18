using System;
using BIO_Q2_GRID.lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace q2.Tests
{
    [TestClass]
    public class MarkScheme
    {
        [TestMethod]
        public void TestGiven()
        {
            Grid g = new Grid(4, 10, 14, 23,47);
            
            Assert.AreEqual("8 2", g.Play());
        }

        [TestMethod]
        public void TestQ1()
        {
            Grid g = new Grid(4, 10, 14, 23,46);
            
            Assert.AreEqual("7 2", g.Play());
        }
    }
}
