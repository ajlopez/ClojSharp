namespace ClojSharp.Core.Tests
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
        [ExpectedException(typeof(ArityException))]
        public void RaiseArityExceptionWhenEvaluateDivideWithoutArguments()
        {
            Assert.AreEqual(1, this.Evaluate("(/)"));
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
