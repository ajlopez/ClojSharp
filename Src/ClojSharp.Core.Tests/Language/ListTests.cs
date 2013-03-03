namespace ClojSharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using ClojSharp.Core.SpecialForms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ListTests
    {
        [TestMethod]
        public void MakeList()
        {
            List list = new List(1, 2);

            Assert.AreEqual(1, list.First);
            Assert.AreEqual(2, list.Rest);
        }

        [TestMethod]
        public void EvaluateAddIntegers()
        {
            Context context = new Context();
            context.SetValue("+", new Add());
            List list = new List(new Symbol("+"), new List(1, new List(2, null)));

            Assert.AreEqual(3, list.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateAddSymbols()
        {
            Context context = new Context();
            context.SetValue("+", new Add());
            context.SetValue("one", 1);
            context.SetValue("two", 2);
            List list = new List(new Symbol("+"), new List(new Symbol("one"), new List(new Symbol("two"), null)));

            Assert.AreEqual(3, list.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateDefIntegerVariable()
        {
            Context context = new Context();
            context.SetValue("def", new Def());
            List list = new List(new Symbol("def"), new List(new Symbol("one"), new List(1, null)));

            var result = list.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Var));

            var var = (Var)result;

            Assert.AreEqual("one", var.Name);

            Assert.AreEqual(1, context.GetValue("one"));
        }
    }
}
