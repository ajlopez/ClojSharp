namespace ClojSharp.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Compiler;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using ClojSharp.Core.SpecialForms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [DeploymentItem("Src", "Src")]
    public class CoreTests
    {
        private Machine machine;

        [TestInitialize]
        public void Setup()
        {
            this.machine = new Machine();
            this.machine.EvaluateFile("Src\\core.clj");
        }

        [TestMethod]
        public void MachineHasMacros()
        {
            this.IsMacro("defmacro");
            this.IsMacro("defn");
        }

        [TestMethod]
        public void DefineAndEvaluateFunction()
        {
            this.Evaluate("(defn incr [x] (+ x 1))");
            Assert.AreEqual(2, this.Evaluate("(incr 1)"));
        }

        private void IsMacro(string name)
        {
            var result = this.machine.RootContext.GetValue(name);
            Assert.IsNotNull(result, name);
            Assert.IsInstanceOfType(result, typeof(IForm), name);
            Assert.IsInstanceOfType(result, typeof(Macro), name);
        }

        private object Evaluate(string text)
        {
            return this.Evaluate(text, this.machine.RootContext);
        }

        private object Evaluate(string text, IContext context)
        {
            Parser parser = new Parser(text);
            var expr = parser.ParseExpression();
            Assert.IsNull(parser.ParseExpression());
            return Machine.Evaluate(expr, context);
        }
    }
}
