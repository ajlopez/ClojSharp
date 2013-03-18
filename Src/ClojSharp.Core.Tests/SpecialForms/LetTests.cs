namespace ClojSharp.Core.Tests.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Compiler;
    using ClojSharp.Core.Language;
    using ClojSharp.Core.SpecialForms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LetTests
    {
        [TestMethod]
        public void LetOneVariable()
        {
            Parser parser = new Parser("(let [x 1] x)");
            Context context = new Context();
            context.SetValue("let", new Let());
            var list = (List)parser.ParseExpression();

            var result = list.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void LetTwoVariables()
        {
            Parser parser = new Parser("(let [x 1 y x] y)");
            Context context = new Context();
            context.SetValue("let", new Let());
            var list = (List)parser.ParseExpression();

            var result = list.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }
    }
}
