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
    public class MFnTests
    {
        private Context context;

        [TestInitialize]
        public void Setup()
        {
            this.context = new Context();
            this.context.SetValue("mfn", new MFn());
            this.context.SetValue("cons", new Cons());
            this.context.SetValue("quote", new Quote());
            this.context.SetValue("list", new ListForm());
        }

        [TestMethod]
        public void MakeList()
        {
            var result = this.EvaluateList("((mfn [x] (cons 'list (cons x nil))) 1)");

            Assert.IsNotNull(result);
            Assert.AreEqual("(1)", result.ToString());
        }

        private object EvaluateList(string text)
        {
            Parser parser = new Parser(text);
            var list = (List)parser.ParseExpression();
            return list.Evaluate(this.context);
        }
    }
}
