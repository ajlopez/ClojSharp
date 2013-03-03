namespace ClojSharp.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContextTests
    {
        [TestMethod]
        public void GetNullIfUndefined()
        {
            Context context = new Context();

            Assert.IsNull(context.GetValue("name"));
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            Context context = new Context();

            context.SetValue("one", 1);
            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void GetValueFromParent()
        {
            Context parent = new Context();
            Context context = new Context(parent);

            parent.SetValue("one", 1);
            Assert.AreEqual(1, context.GetValue("one"));
            Assert.AreEqual(1, parent.GetValue("one"));
        }

        [TestMethod]
        public void SetRootValue()
        {
            Context parent = new Context();
            Context context = new Context(parent);

            context.SetRootValue("one", 1);
            Assert.AreEqual(1, context.GetValue("one"));
            Assert.AreEqual(1, parent.GetValue("one"));
        }
    }
}
