namespace ClojSharp.Core.Tests.SpecialForms
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClojSharp.Core.Compiler;
    using ClojSharp.Core.Forms;

    [TestClass]
    public class MacroTests
    {
        [TestMethod]
        public void MakeAndEvaluateList()
        {
            Machine machine = new Machine();
            Parser parser = new Parser("(cons 'list (cons x (cons y nil)))");
            object body = parser.ParseExpression();
            Macro macro = new Macro(machine.RootContext, new string[] { "x", "y" }, body);
            var result = macro.Evaluate(machine.RootContext, new object[] { 1, 2 });
            Assert.IsNotNull(result);
            Assert.AreEqual("(1 2)", result.ToString());
        }
    }
}
