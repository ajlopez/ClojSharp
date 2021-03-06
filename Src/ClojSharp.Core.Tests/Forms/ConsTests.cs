﻿namespace ClojSharp.Core.Tests.Forms
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
    public class ConsTests
    {
        [TestMethod]
        public void ConsIntegerToNil()
        {
            Cons cons = new Cons();
            var result = cons.Evaluate(null, new object[] { 1, null });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ISeq));
            
            var seq = (ISeq)result;
            Assert.AreEqual(1, seq.First);
            Assert.IsNull(seq.Next);
        }

        [TestMethod]
        public void ConsIntegerToList()
        {
            Cons cons = new Cons();
            var list = new List(2, null);
            var result = cons.EvaluateForm(null, new object[] { 1, list });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ISeq));
            Assert.AreEqual("(1 2)", result.ToString());
        }

        [TestMethod]
        public void ConsIntegerToVector()
        {
            Cons cons = new Cons();
            var vector = new Vector(new object[] { 2, 3 });
            var result = cons.EvaluateForm(null, new object[] { 1, vector });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ISeq));
            Assert.AreEqual("(1 2 3)", result.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RaiseWhenSecondArgumentIsNotAnISeq()
        {
            Cons cons = new Cons();
            cons.Evaluate(null, new object[] { 1, 2 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseWhenOnlyOneArgument()
        {
            Cons cons = new Cons();
            cons.Evaluate(null, new object[] { 1 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseWhenThreeArgument()
        {
            Cons cons = new Cons();
            cons.Evaluate(null, new object[] { 1, 2, 3 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseWhenNoArgument()
        {
            Cons cons = new Cons();
            cons.Evaluate(null, new object[] { });
        }
    }
}
