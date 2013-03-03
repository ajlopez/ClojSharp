namespace ClojSharp.Core.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClojSharp.Core.Forms;

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
    }
}
