namespace ClojSharp.Core.Tests.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void AddIntegerAndReal()
        {
            Add add = new Add();

            Assert.AreEqual(3.5, add.Evaluate(null, new object[] { 1, 2.5 }));
        }

        [TestMethod]
        public void AddRealAndInteger()
        {
            Add add = new Add();

            Assert.AreEqual(3.2, add.Evaluate(null, new object[] { 1.2, 2 }));
        }

        [TestMethod]
        public void AddTwoReals()
        {
            Add add = new Add();

            Assert.AreEqual(3.3, add.Evaluate(null, new object[] { 1.2, 2.1 }));
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
