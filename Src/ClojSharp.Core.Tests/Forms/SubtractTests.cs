﻿namespace ClojSharp.Core.Tests.Forms
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClojSharp.Core.Forms;

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
        public void SubtractThreeIntegers()
        {
            Subtract subtract = new Subtract();
            Assert.AreEqual(-4, subtract.Evaluate(null, new object[] { 1, 2, 3 }));
        }
    }
}
