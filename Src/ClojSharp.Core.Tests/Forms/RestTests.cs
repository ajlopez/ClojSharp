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
    public class RestTests
    {
        [TestMethod]
        public void RestSimpleList()
        {
            Rest rest = new Rest();
            Machine machine = new Machine();

            Assert.AreSame(EmptyList.Instance, rest.Evaluate(machine.RootContext, new object[] { new List(new Symbol("list"), new List(1, null)) }));
        }

        [TestMethod]
        public void RestToNil()
        {
            Rest rest = new Rest();
            Assert.AreSame(EmptyList.Instance, rest.Evaluate(null, new object[] { null }));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RaiseIfArgumentIsNotAnISeq()
        {
            Rest rest = new Rest();
            rest.Evaluate(null, new object[] { 1 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseIfNullArguments()
        {
            Rest rest = new Rest();
            rest.Evaluate(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseIfNoArgument()
        {
            Rest rest = new Rest();
            rest.Evaluate(null, new object[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseIfTwoArguments()
        {
            Rest rest = new Rest();
            rest.Evaluate(null, new object[] { 1, 2 });
        }
    }
}
