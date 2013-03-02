namespace ClojSharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
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
    }
}
