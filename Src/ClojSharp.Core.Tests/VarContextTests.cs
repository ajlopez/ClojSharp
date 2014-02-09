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
            VarContext context = new VarContext();

            Assert.IsNull(context.GetValue("name"));
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            VarContext context = new VarContext();

            context.SetValue("one", 1);
            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void GetValueFromParent()
        {
            VarContext parent = new VarContext();
            Context context = new Context(parent);

            parent.SetValue("one", 1);
            Assert.AreEqual(1, context.GetValue("one"));
            Assert.AreEqual(1, parent.GetValue("one"));
        }

        [TestMethod]
        public void GetVar()
        {
            VarContext parent = new VarContext();
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
