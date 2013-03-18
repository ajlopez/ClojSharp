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
    public class LetTests
    {
        private Context context;

        [TestInitialize]
        public void Setup()
        {
            this.context = new Context();
            this.context.SetValue("let", new Let());
        }

        [TestMethod]
        public void LetOneVariable()
        {
            var result = this.EvaluateList("(let [x 1] x)");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void LetTwoVariables()
        {
            var result = this.EvaluateList("(let [x 1 y x] x)");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ExceptionWhenOddNumberOfForms()
        {
            try
            {
                this.EvaluateList("(let [x 1 2] x)");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(IllegalArgumentException));
                Assert.AreEqual("let requires an even number of forms in binding vector", ex.Message);
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
