using Microsoft.VisualStudio.TestTools.UnitTesting;
using HoustonTest;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using System;

namespace HoustonTest
{
    [TestClass]
    public class HoustonTests 
    {
        [TestMethod]
        public void TestPrintTest()
        {
            var c1 = new Houston.Class1();
            Assert.AreEqual(c1.PrintsTest(), "Test");
        }


        [TestMethod]
        public void TestGetDNS()
        {
            var c1 = new Houston.Class1();
            var name = c1.GetAddress("google.com").Result;
            Console.WriteLine("test");
            Assert.AreEqual(name, "");

        }
    }
}
