namespace ClojSharp.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MachineTests
    {
        private Machine machine;

        [TestInitialize]
        public void Setup()
        {
            this.machine = new Machine();
        }

        [TestMethod]
        public void MachineHasRootContext()
        {
            Assert.IsNotNull(this.machine.RootContext);
        }

        [TestMethod]
        public void MachineHasDefinedForms()
        {
            this.IsForm("+");
            this.IsForm("-");
            this.IsForm("*");
            this.IsForm("/");
            this.IsForm("cons");
            this.IsForm("list");
            this.IsForm("first");
            this.IsForm("rest");
            this.IsForm("next");
        }

        [TestMethod]
        public void MachineHasDefinedSpecialForms()
        {
            this.IsSpecialForm("def");
            this.IsSpecialForm("fn");
            this.IsSpecialForm("quote");
            this.IsSpecialForm("let");
            this.IsSpecialForm("if");
        }

        [TestMethod]
        public void MachineEvaluateObjects()
        {
            Assert.AreEqual(3, Machine.Evaluate(3, null));
            Assert.AreEqual("foo", Machine.Evaluate("foo", null));
            Assert.IsNull(Machine.Evaluate(null, null));
        }

        [TestMethod]
        public void MachineEvaluateSymbolInContext()
        {
            Context context = new Context();
            context.SetValue("one", 1);

            Assert.AreEqual(1, Machine.Evaluate(new Symbol("one"), context));
        }

        private void IsSpecialForm(string name)
        {
            var result = this.machine.RootContext.GetValue(name);
            Assert.IsNotNull(result, name);
            Assert.IsInstanceOfType(result, typeof(IForm), name);
            Assert.IsNotInstanceOfType(result, typeof(BaseForm), name);
        }

        private void IsForm(string name)
        {
            var result = this.machine.RootContext.GetValue(name);
            Assert.IsNotNull(result, name);
            Assert.IsInstanceOfType(result, typeof(IForm), name);
            Assert.IsInstanceOfType(result, typeof(BaseForm), name);
        }
    }
}
