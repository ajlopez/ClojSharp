namespace ClojSharp.Core.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClojSharp.Core.Language;

    [TestClass]
    public class VarTests
    {
        [TestMethod]
        public void CreateWithName()
        {
            Var var = new Var("foo");

            Assert.AreEqual("foo", var.Name);
            Assert.AreEqual("user/foo", var.FullName);
            Assert.IsNull(var.Value);
        }
    }
}
