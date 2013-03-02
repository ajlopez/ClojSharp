﻿namespace ClojSharp.Core.Tests.Forms
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClojSharp.Core.Forms;

    [TestClass]
    public class AddTests
    {
        [TestMethod]
        public void AddTwoIntegers()
        {
            Add add = new Add();

            Assert.AreEqual(3, add.Evaluate(null, new object[] { 1, 2 }));
        }

        [TestMethod]
        public void AddNoIntegerIsZero()
        {
            Add add = new Add();

            Assert.AreEqual(0, add.Evaluate(null, new object[] { }));
        }

        [TestMethod]
        public void AddNullIsZero()
        {
            Add add = new Add();

            Assert.AreEqual(0, add.Evaluate(null, null));
        }

        [TestMethod]
        public void AddAnInteger()
        {
            Add add = new Add();

            Assert.AreEqual(5, add.Evaluate(null, new object[] { 5 }));
        }
    }
}
