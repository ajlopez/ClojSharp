namespace ClojSharp.Core.Tests.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Compiler;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using ClojSharp.Core.SpecialForms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LoopTests
    {
        private Context context;

        [TestInitialize]
        public void Setup()
        {
            this.context = new Context();
            this.context.SetValue("loop", new Loop());
            this.context.SetValue("recur", new Recur());
            this.context.SetValue("if", new If());
        }

        [TestMethod]
        public void LoopOneVariable()
        {
            var result = this.EvaluateList("(loop [x 1] x)");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void LoopTwoVariables()
        {
            var result = this.EvaluateList("(loop [x 1 y x] x)");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void LoopWithRecur()
        {
            var result = this.EvaluateList("(loop [x true] (if x (recur false) 10))");

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void ExceptionWhenOddNumberOfForms()
        {
            try
            {
                this.EvaluateList("(loop [x 1 2] x)");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(IllegalArgumentException));
                Assert.AreEqual("loop requires an even number of forms in binding vector", ex.Message);
            }
        }

        [TestMethod]
        public void ExceptionWhenFirstArgumentIsNotAVector()
        {
            try
            {
                this.EvaluateList("(loop 1 2 3)");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(IllegalArgumentException));
                Assert.AreEqual("loop requires a vector for its bindings", ex.Message);
            }
        }

        [TestMethod]
        public void ExceptionWhenNoArguments()
        {
            try
            {
                this.EvaluateList("(loop)");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArityException));
                Assert.AreEqual("Wrong number of args (0) passed to ClojSharp.Core.SpecialForms.Loop", ex.Message);
            }
        }

        private object EvaluateList(string text)
        {
            Parser parser = new Parser(text);
            var list = (List)parser.ParseExpression();
            return list.Evaluate(this.context);
        }
    }
}
