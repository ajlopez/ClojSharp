namespace ClojSharp.Core.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
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
    }
}
