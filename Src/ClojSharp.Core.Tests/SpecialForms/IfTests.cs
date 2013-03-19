namespace ClojSharp.Core.Tests.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Compiler;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;
    using ClojSharp.Core.SpecialForms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class IfTests
    {
        private Context context;

        [TestInitialize]
        public void Setup()
        {
            this.context = new Context();
            this.context.SetValue("if", new If());
        }

        [TestMethod]
        public void IfTrue()
        {
            var result = this.EvaluateList("(if 1 2 3)");

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void IfNil()
        {
            var result = this.EvaluateList("(if nil 2 3)");

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void IfFalse()
        {
            var result = this.EvaluateList("(if false 2 3)");

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void ExceptionWhenTooManyArguments()
        {
            try
            {
                this.EvaluateList("(if false 2 3 4)");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(RuntimeException));
                Assert.AreEqual("Too many arguments to if", ex.Message);
            }
        }

        [TestMethod]
        public void ExceptionWhenNoArguments()
        {
            try
            {
                this.EvaluateList("(if)");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(RuntimeException));
                Assert.AreEqual("Too few arguments to if", ex.Message);
            }
        }

        [TestMethod]
        public void ExceptionWhenOnlyOneArgument()
        {
            try
            {
                this.EvaluateList("(if 1)");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(RuntimeException));
                Assert.AreEqual("Too few arguments to if", ex.Message);
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
