namespace ClojSharp.Core.Tests.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MultiplyTests
    {
        [TestMethod]
        public void MultiplyTwoIntegers()
        {
            Multiply multiply = new Multiply();

            Assert.AreEqual(6, multiply.Evaluate(null, new object[] { 3, 2 }));
        }

        [TestMethod]
        public void MultiplyTwoIntegersGivingLong()
        {
            Multiply multiply = new Multiply();

            Assert.AreEqual(2 * (long)int.MaxValue, multiply.Evaluate(null, new object[] { int.MaxValue, 2 }));
        }

        [TestMethod]
        public void MultiplyIntegerAndReal()
        {
            Multiply multiply = new Multiply();

            Assert.AreEqual(3 * 2.5, multiply.Evaluate(null, new object[] { 3, 2.5 }));
        }

        [TestMethod]
        public void MultiplyRealAndInteger()
        {
            Multiply multiply = new Multiply();

            Assert.AreEqual(2.5 * 3, multiply.Evaluate(null, new object[] { 2.5, 3 }));
        }

        [TestMethod]
        public void MultiplyTwoReals()
        {
            Multiply multiply = new Multiply();

            Assert.AreEqual(1.2 * 2.1, multiply.Evaluate(null, new object[] { 1.2, 2.1 }));
        }

        [TestMethod]
        public void MultiplyNoIntegerIsOne()
        {
            Multiply multiply = new Multiply();

            Assert.AreEqual(1, multiply.Evaluate(null, new object[] { }));
        }

        [TestMethod]
        public void MultiplyNullIsOne()
        {
            Multiply multiply = new Multiply();

            Assert.AreEqual(1, multiply.Evaluate(null, null));
        }

        [TestMethod]
        public void MultiplyAnInteger()
        {
            Multiply multiply = new Multiply();

            Assert.AreEqual(5, multiply.Evaluate(null, new object[] { 5 }));
        }
    }
}
