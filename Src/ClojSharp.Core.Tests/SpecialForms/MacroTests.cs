namespace ClojSharp.Core.Tests.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Compiler;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.SpecialForms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClojSharp.Core.Exceptions;

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

        [TestMethod]
        public void RaiseInvalidArity()
        {
            Machine machine = new Machine();
            Macro macro = new Macro(machine.RootContext, new string[] { "x", "y" }, null, 1);

            try
            {
                macro.Evaluate(machine.RootContext, new object[] { 1 });
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArityException));
                Assert.AreEqual("Wrong number of args (1) passed to ClojSharp.Core.SpecialForms.Macro", ex.Message);
            }
        }

        [TestMethod]
        public void RaiseInvalidArityWithVariableArity()
        {
            Machine machine = new Machine();
            Macro macro = new Macro(machine.RootContext, new string[] { "x", "y" }, "rest", 1);

            try
            {
                macro.Evaluate(machine.RootContext, new object[] { 1 });
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArityException));
                Assert.AreEqual("Wrong number of args (1) passed to ClojSharp.Core.SpecialForms.Macro", ex.Message);
            }
        }
    }
}
