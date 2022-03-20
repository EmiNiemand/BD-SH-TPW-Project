using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPW.Tests
{
    [TestClass()]
    public class HelloWorldTests
    {
        [TestMethod()]
        public void GetGreetingTest()
        {
            HelloWorld helloWorld = new HelloWorld();
            Assert.AreEqual(helloWorld.GetGreeting("Bartek"), "Hello Bartek");
        }

        [TestMethod()]
        public void SumTest()
        {
            HelloWorld helloWorld = new HelloWorld();
            Assert.AreEqual(helloWorld.Sum(2, 5), 7);
        }
    }
}