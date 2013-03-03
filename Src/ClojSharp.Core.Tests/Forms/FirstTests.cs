namespace ClojSharp.Core.Tests.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClojSharp.Core.Exceptions;

    [TestClass]
    public class FirstTests
    {
        [TestMethod]
        public void FirstInSimpleList()
        {
            First first = new First();
            Machine machine = new Machine();

            Assert.AreEqual(1, first.Evaluate(machine.RootContext, new object[] { new List(new Symbol("list"), new List(1, null)) }));
        }

        [TestMethod]
        public void FirstToNil()
        {
            First first = new First();
            Assert.IsNull(first.Evaluate(null, new object[] { null }));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RaiseIfArgumentIsNotAnISeq()
        {
            First first = new First();
            first.Evaluate(null, new object[] { 1 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseIfNullArguments()
        {
            First first = new First();
            first.Evaluate(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseIfNoArgument()
        {
            First first = new First();
            first.Evaluate(null, new object[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseIfTwoArguments()
        {
            First first = new First();
            first.Evaluate(null, new object[] { 1, 2 });
        }
    }
}
