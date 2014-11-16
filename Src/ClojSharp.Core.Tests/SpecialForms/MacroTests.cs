﻿namespace ClojSharp.Core.Tests.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Compiler;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.SpecialForms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MacroTests
    {
        [TestMethod]
        public void MakeAndEvaluateList()
        {
            Machine machine = new Machine();
            Parser parser = new Parser("(cons 'list (cons x (cons y nil)))");
            object body = parser.ParseExpression();
            Macro macro = new Macro(machine.RootContext, new string[] { "x", "y" }, null, body);
            var result = macro.Evaluate(machine.RootContext, new object[] { 1, 2 });
            Assert.IsNotNull(result);
            Assert.AreEqual("(1 2)", result.ToString());
        }
    }
}
