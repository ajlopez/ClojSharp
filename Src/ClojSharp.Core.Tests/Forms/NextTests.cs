namespace ClojSharp.Core.Tests.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NextTests
    {
        [TestMethod]
        public void NextSimpleList()
        {
            Next next = new Next();
            Machine machine = new Machine();

            Assert.IsNull(next.Evaluate(machine.RootContext, new object[] { new List(new Symbol("list"), new List(1, null)) }));
        }

        [TestMethod]
        public void NextToNil()
        {
            Next next = new Next();
            Assert.IsNull(next.Evaluate(null, new object[] { null }));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RaiseIfArgumentIsNotAnISeq()
        {
            Next next = new Next();
            next.Evaluate(null, new object[] { 1 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseIfNullArguments()
        {
            Next next = new Next();
            next.Evaluate(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseIfNoArgument()
        {
            Next next = new Next();
            next.Evaluate(null, new object[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseIfTwoArguments()
        {
            Next next = new Next();
            next.Evaluate(null, new object[] { 1, 2 });
        }
    }
}
