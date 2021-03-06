﻿namespace ClojSharp.Core.Tests.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FunctionTests
    {
        [TestMethod]
        public void FunctionEvaluateString()
        {
            Function function = new Function(null, null, "foo");

            Assert.AreEqual("foo", function.Evaluate(null, null));
        }

        [TestMethod]
        public void FunctionEvaluateSymbolInClosure()
        {
            Context closure = new Context();
            closure.SetValue("one", 1);
            Function function = new Function(closure, null, new Symbol("one"));

            Assert.AreEqual(1, function.Evaluate(null, null));
        }

        [TestMethod]
        public void FunctionEvaluateAdd()
        {
            Machine machine = new Machine();
            var list = new List(new Symbol("+"), new List(1, new List(2, null)));
            Function function = new Function(machine.RootContext, null, list);

            Assert.AreEqual(3, function.Evaluate(null, null));
        }

        [TestMethod]
        public void FunctionEvaluateAddToArgument()
        {
            Machine machine = new Machine();
            var list = new List(new Symbol("+"), new List(1, new List(new Symbol("x"), null)));
            Function function = new Function(machine.RootContext, new string[] { "x" }, list);

            Assert.AreEqual(3, function.Evaluate(null, new object[] { 2 }));
        }

        [TestMethod]
        public void FunctionEvaluateAddArgumentAndFreeVariable()
        {
            Machine machine = new Machine();
            machine.RootContext.SetValue("one", 1);
            var list = new List(new Symbol("+"), new List(new Symbol("one"), new List(new Symbol("x"), null)));
            Function function = new Function(machine.RootContext, new string[] { "x" }, list);

            Assert.AreEqual(3, function.Evaluate(null, new object[] { 2 }));
        }

        [TestMethod]
        public void FunctionWithVariableArity()
        {
            Function function = new Function(null, null, "rest", null);

            Assert.IsTrue(function.VariableArity);
        }

        [TestMethod]
        public void EvaluateFunctionWithVariableArity()
        {
            Function function = new Function(null, null, "rest", new Symbol("rest"));

            var result = function.Evaluate(null, new object[] { 1, 2, 3 });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ISeq));

            var seq = (ISeq)result;

            Assert.AreEqual(1, seq.First);
            Assert.AreEqual(2, seq.Next.First);
            Assert.AreEqual(3, seq.Next.Next.First);
            Assert.IsNull(seq.Next.Next.Next);
        }

        [TestMethod]
        public void EvaluateFunctionWithArgumentsAndVariableArity()
        {
            Function function = new Function(null, new string[] { "x", "y" }, "rest", new Symbol("rest"));

            var result = function.Evaluate(null, new object[] { 1, 2, 3, 4 });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ISeq));

            var seq = (ISeq)result;

            Assert.AreEqual(3, seq.First);
            Assert.AreEqual(4, seq.Next.First);
            Assert.IsNull(seq.Next.Next);
        }
    }
}
