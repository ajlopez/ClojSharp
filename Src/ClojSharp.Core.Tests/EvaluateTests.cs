namespace ClojSharp.Core.Tests
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
        [TestMethod]
        public void EvaluateInteger()
        {
            Machine machine = new Machine();
            Parser parser = new Parser("123");
            var expr = parser.ParseExpression();
            Assert.AreEqual(123, machine.Evaluate(expr, null));
        }

        [TestMethod]
        public void EvaluateSymbolInContext()
        {
            Machine machine = new Machine();
            Context context = new Context();
            context.SetValue("one", 1);
            Parser parser = new Parser("one");
            var expr = parser.ParseExpression();
            Assert.AreEqual(1, machine.Evaluate(expr, context));
        }
    }
}
