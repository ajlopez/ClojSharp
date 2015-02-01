namespace ClojSharp.Core.Tests.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DivideTests
    {
        [TestMethod]
        public void DivideTwoIntegers()
        {
            Divide divide = new Divide();

            Assert.AreEqual(2, divide.Evaluate(null, new object[] { 10, 5 }));
        }

        [TestMethod]
        public void DivideIntegerAndReal()
        {
            Divide divide = new Divide();

            Assert.AreEqual(3 / 2.5, divide.Evaluate(null, new object[] { 3, 2.5 }));
        }

        [TestMethod]
        public void DivideRealAndInteger()
        {
            Divide divide = new Divide();

            Assert.AreEqual(2.5 / 3, divide.Evaluate(null, new object[] { 2.5, 3 }));
        }

        [TestMethod]
        public void DivideTwoReals()
        {
            Divide divide = new Divide();

            Assert.AreEqual(1.2 / 2.1, divide.Evaluate(null, new object[] { 1.2, 2.1 }));
        }

        [TestMethod]
        public void DivideAnInteger()
        {
            Divide divide = new Divide();

            Assert.AreEqual(1.0 / 5, divide.Evaluate(null, new object[] { 5 }));
        }
    }
}
