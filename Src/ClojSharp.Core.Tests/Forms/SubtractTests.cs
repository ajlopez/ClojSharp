namespace ClojSharp.Core.Tests.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SubtractTests
    {
        [TestMethod]
        public void SubtractTwoIntegers()
        {
            Subtract subtract = new Subtract();
            Assert.AreEqual(-1, subtract.Evaluate(null, new object[] { 1, 2 }));
        }

        [TestMethod]
        public void SubtractAnInteger()
        {
            Subtract subtract = new Subtract();
            Assert.AreEqual(-1, subtract.Evaluate(null, new object[] { 1 }));
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseIfSubtractNoInteger()
        {
            Subtract subtract = new Subtract();
            subtract.Evaluate(null, new object[] { });
        }

        [TestMethod]
        public void SubtractThreeIntegers()
        {
            Subtract subtract = new Subtract();
            Assert.AreEqual(-4, subtract.Evaluate(null, new object[] { 1, 2, 3 }));
        }
    }
}
