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
        [TestMethod]
        public void MachineHasRootContext()
        {
            Machine machine = new Machine();

            Assert.IsNotNull(machine.RootContext);
        }

        [TestMethod]
        public void MachineHasDefinedForms()
        {
            Machine machine = new Machine();

            var result = machine.RootContext.GetValue("+");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IForm));
            Assert.IsInstanceOfType(result, typeof(BaseForm));

            result = machine.RootContext.GetValue("-");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IForm));
            Assert.IsInstanceOfType(result, typeof(BaseForm));
        }

        [TestMethod]
        public void MachineHasDefinedSpecialForms()
        {
            Machine machine = new Machine();

            var result = machine.RootContext.GetValue("def");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IForm));
            Assert.IsNotInstanceOfType(result, typeof(BaseForm));
        }

        [TestMethod]
        public void MachineEvaluateObjects()
        {
            Machine machine = new Machine();

            Assert.AreEqual(3, machine.Evaluate(3, null));
            Assert.AreEqual("foo", machine.Evaluate("foo", null));
            Assert.IsNull(machine.Evaluate(null, null));
        }

        [TestMethod]
        public void MachineEvaluateSymbolInContext()
        {
            Machine machine = new Machine();
            Context context = new Context();
            context.SetValue("one", 1);

            Assert.AreEqual(1, machine.Evaluate(new Symbol("one"), context));
        }
    }
}
