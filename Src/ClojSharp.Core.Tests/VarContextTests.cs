namespace ClojSharp.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class VarContextTests
    {
        [TestMethod]
        public void GetNullIfUndefined()
        {
            Machine machine = new Machine();
            VarContext context = new VarContext(machine);

            Assert.IsNull(context.GetValue("name"));
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            Machine machine = new Machine();
            VarContext context = new VarContext(machine);

            context.SetValue("one", 1);
            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void GetValueFromParent()
        {
            Machine machine = new Machine();
            VarContext parent = new VarContext(machine);
            Context context = new Context(parent);

            parent.SetValue("one", 1);
            Assert.AreEqual(1, context.GetValue("one"));
            Assert.AreEqual(1, parent.GetValue("one"));
        }

        [TestMethod]
        public void GetVar()
        {
            Machine machine = new Machine();
            VarContext parent = new VarContext(machine);
            Context context = new Context(parent);

            parent.SetValue("one", 1);
            Assert.AreEqual(1, context.GetValue("one"));
            Assert.AreEqual(1, parent.GetValue("one"));

            var result = parent.GetVar("one");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Value);
        }
    }
}
