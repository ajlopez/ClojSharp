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
    public class FnTests
    {
        private Context context;

        [TestInitialize]
        public void Setup()
        {
            this.context = new Context();
            this.context.SetValue("fn", new Fn());
            this.context.SetValue("recur", new Recur());
            this.context.SetValue("if", new If());
            this.context.SetValue("+", new Add());
        }

        [TestMethod]
        public void Identity()
        {
            var result = this.EvaluateList("((fn [x] x) 1)");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Increment()
        {
            var result = this.EvaluateList("((fn [x] (+ x 1)) 1)");

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void UsingRecur()
        {
            var result = this.EvaluateList("((fn [x] (if x (recur false) 10)) true)");

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result);
        }

        private object EvaluateList(string text)
        {
            Parser parser = new Parser(text);
            var list = (List)parser.ParseExpression();
            return list.Evaluate(this.context);
        }
    }
}
