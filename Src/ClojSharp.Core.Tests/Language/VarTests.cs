namespace ClojSharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        [TestMethod]
        public void CreateWithNameAndValue()
        {
            Var var = new Var("one", 1);

            Assert.AreEqual("one", var.Name);
            Assert.AreEqual("user/one", var.FullName);
            Assert.AreEqual(1, var.Value);
        }
    }
}
