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

        [TestMethod]
        public void DefVariableWithoutInitialValue()
        {
            Context context = new Context();
            Symbol symbol = new Symbol("one");
            Def def = new Def();

            var result = def.Evaluate(context, new object[] { symbol });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Var));

            var var = (Var)result;

            Assert.AreEqual("one", var.Name);

            Assert.IsNull(context.GetValue("one"));
        }

        [TestMethod]
        public void DefVariableFromASymbol()
        {
            Context context = new Context();
            context.SetValue("uno", 1);
            Symbol symbol = new Symbol("one");
            Symbol uno = new Symbol("uno");
            Def def = new Def();

            var result = def.Evaluate(context, new object[] { symbol, uno });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Var));

            var var = (Var)result;

            Assert.AreEqual("one", var.Name);

            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void DefIntegerVariableAtRootContext()
        {
            Context root = new Context();
            Context context = new Context(root);
            Symbol symbol = new Symbol("one");
            Def def = new Def();

            var result = def.Evaluate(context, new object[] { symbol, 1 });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Var));

            var var = (Var)result;

            Assert.AreEqual("one", var.Name);

            Assert.AreEqual(1, context.GetValue("one"));
            Assert.AreEqual(1, root.GetValue("one"));
        }
    }
}
