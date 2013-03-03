﻿namespace ClojSharp.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Compiler;
    using ClojSharp.Core.Exceptions;
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
        public void EvaluateAdd()
        {
            Assert.AreEqual(3, this.Evaluate("(+ 1 2)"));
            Assert.AreEqual(6, this.Evaluate("(+ 1 2 3)"));
            Assert.AreEqual(5, this.Evaluate("(+ 5)"));
            Assert.AreEqual(0, this.Evaluate("(+)"));
        }

        [TestMethod]
        public void EvaluateSubtract()
        {
            Assert.AreEqual(-1, this.Evaluate("(- 1 2)"));
            Assert.AreEqual(-4, this.Evaluate("(- 1 2 3)"));
            Assert.AreEqual(-5, this.Evaluate("(- 5)"));
        }

        [TestMethod]
        public void EvaluateMultiply()
        {
            Assert.AreEqual(1, this.Evaluate("(*)"));
            Assert.AreEqual(2, this.Evaluate("(* 2)"));
            Assert.AreEqual(6, this.Evaluate("(* 2 3)"));
            Assert.AreEqual(24, this.Evaluate("(* 2 3 4)"));
        }

        [TestMethod]
        public void EvaluateDivide()
        {
            Assert.AreEqual(1, this.Evaluate("(/ 1)"));
            Assert.AreEqual(2, this.Evaluate("(/ 4 2)"));
            Assert.AreEqual(2, this.Evaluate("(/ 8 2 2)"));
        }

        [TestMethod]
        public void EvaluateSimpleFn()
        {
            Assert.AreEqual(1, this.Evaluate("((fn [] 1))"));
        }

        [TestMethod]
        public void EvaluateFnWithSimpleBody()
        {
            Assert.AreEqual(3, this.Evaluate("((fn [] (+ 1 2)))"));
        }

        [TestMethod]
        public void EvaluateFnWithArgument()
        {
            Assert.AreEqual(3, this.Evaluate("((fn [x] (+ 1 x)) 2)"));
        }

        [TestMethod]
        public void EvaluateFnWithArgumentAndFreeVariable()
        {
            this.machine.RootContext.SetValue("one", 1);
            Assert.AreEqual(3, this.Evaluate("((fn [x] (+ one x)) 2)"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseWhenAdditionalFirstArgument()
        {
            Assert.AreEqual(1, this.Evaluate("((fn [] 1) 2)"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseWhenAdditionalSecondArgument()
        {
            Assert.AreEqual(1, this.Evaluate("((fn [x] (+ 1 x)) 2 3)"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseWhenMissingFirstArgument()
        {
            Assert.AreEqual(1, this.Evaluate("((fn [x] (+ 1 x)))"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseArityExceptionWhenEvaluateDivideWithoutArguments()
        {
            Assert.AreEqual(1, this.Evaluate("(/)"));
        }

        [TestMethod]
        public void EvaluateQuoteInteger()
        {
            Assert.AreEqual(1, this.Evaluate("(quote 1)"));
        }

        [TestMethod]
        public void EvaluateQuoteList()
        {
            Assert.AreEqual("(1 2)", this.Evaluate("(quote (1 2))").ToString());
            Assert.AreEqual("(1 2)", this.Evaluate("'(1 2)").ToString());
        }

        [TestMethod]
        public void EvaluateList()
        {
            Assert.AreEqual("(1 2)", this.Evaluate("(list 1 2)").ToString());
            Assert.AreEqual("(1 2 3)", this.Evaluate("(list 1 2 3)").ToString());
            Assert.AreEqual("()", this.Evaluate("(list)").ToString());
            Assert.AreEqual("(nil)", this.Evaluate("(list nil)").ToString());
        }

        [TestMethod]
        public void EvaluateFirst()
        {
            Assert.AreEqual(1, this.Evaluate("(first (list 1 2))"));
            Assert.IsNull(this.Evaluate("(first nil)"));
            Assert.IsNull(this.Evaluate("(first ())"));
        }

        private object Evaluate(string text)
        {
            return this.Evaluate(text, this.machine.RootContext);
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
