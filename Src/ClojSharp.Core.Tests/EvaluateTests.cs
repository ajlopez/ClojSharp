﻿namespace ClojSharp.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Compiler;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EvaluateTests
    {
        private Machine machine;

        [TestInitialize]
        public void Setup()
        {
            this.machine = new Machine();
        }

        [TestMethod]
        public void EvaluateInteger()
        {
            Assert.AreEqual(123, this.Evaluate("123", null));
        }

        [TestMethod]
        public void EvaluateSymbolInContext()
        {
            Context context = new Context();
            context.SetValue("one", 1);
            Assert.AreEqual(1, this.Evaluate("one", context));
        }

        [TestMethod]
        public void EvaluateAddTwoIntegers()
        {
            Assert.AreEqual(3, this.Evaluate("(+ 1 2)", this.machine.RootContext));
        }

        [TestMethod]
        public void EvaluateAddThreeIntegers()
        {
            Assert.AreEqual(6, this.Evaluate("(+ 1 2 3)", this.machine.RootContext));
        }

        [TestMethod]
        public void EvaluateOneInteger()
        {
            Assert.AreEqual(5, this.Evaluate("(+ 5)", this.machine.RootContext));
        }

        [TestMethod]
        public void EvaluateNoInteger()
        {
            Assert.AreEqual(0, this.Evaluate("(+)", this.machine.RootContext));
        }

        private object Evaluate(string text, Context context)
        {
            Parser parser = new Parser(text);
            var expr = parser.ParseExpression();
            Assert.IsNull(parser.ParseExpression());
            return this.machine.Evaluate(expr, context);
        }
    }
}
