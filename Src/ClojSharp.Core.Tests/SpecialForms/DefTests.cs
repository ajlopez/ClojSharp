﻿namespace ClojSharp.Core.Tests.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;
    using ClojSharp.Core.SpecialForms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DefTests
    {
        [TestMethod]
        public void DefIntegerVariable()
        {
            Context context = new Context();
            Symbol symbol = new Symbol("one");
            Def def = new Def();

            var result = def.Evaluate(context, new object[] { symbol, 1 });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Var));

            var var = (Var)result;

            Assert.AreEqual("one", var.Name);

            Assert.AreEqual(1, context.GetValue("one"));
        }
    }
}
