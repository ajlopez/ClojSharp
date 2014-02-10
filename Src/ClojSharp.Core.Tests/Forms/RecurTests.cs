namespace ClojSharp.Core.Tests.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RecurTests
    {
        [TestMethod]
        public void RecurThreeIntegers()
        {
            Recur list = new Recur();
            var result = list.Evaluate(null, new object[] { 1, 2, 3 });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RecurValues));

            var rv = (RecurValues)result;

            Assert.IsNotNull(rv.Elements);
            Assert.AreEqual(3, rv.Elements.Count);
            Assert.AreEqual(1, rv.Elements[0]);
            Assert.AreEqual(2, rv.Elements[1]);
            Assert.AreEqual(3, rv.Elements[2]);
        }

        [TestMethod]
        public void RecurOneIntegers()
        {
            Recur list = new Recur();
            var result = list.Evaluate(null, new object[] { 1 });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RecurValues));

            var rv = (RecurValues)result;

            Assert.IsNotNull(rv.Elements);
            Assert.AreEqual(1, rv.Elements.Count);
        }

        [TestMethod]
        public void RecurWithNoArguments()
        {
            Recur list = new Recur();
            var result = list.Evaluate(null, new object[] { });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RecurValues));

            var rv = (RecurValues)result;

            Assert.IsNotNull(rv.Elements);
            Assert.AreEqual(0, rv.Elements.Count);
        }

        [TestMethod]
        public void RecurWithNullArguments()
        {
            Recur list = new Recur();
            var result = list.Evaluate(null, null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RecurValues));

            var rv = (RecurValues)result;

            Assert.IsNotNull(rv.Elements);
            Assert.AreEqual(0, rv.Elements.Count);
        }
    }
}
