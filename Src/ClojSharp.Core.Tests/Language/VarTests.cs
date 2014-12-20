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
            Machine machine = new Machine();
            Var var = new Var(machine, "foo");

            Assert.AreEqual("foo", var.Name);
            Assert.AreEqual("user/foo", var.FullName);
            Assert.IsNull(var.Value);

            Assert.IsNotNull(var.Metadata);

            var meta = var.Metadata;

            Assert.AreEqual("foo", meta.GetValue("name"));
            Assert.IsNotNull(meta.GetValue("ns"));
            Assert.IsInstanceOfType(meta.GetValue("ns"), typeof(Namespace));

            var ns = (Namespace)meta.GetValue("ns");
            Assert.AreEqual("user", ns.Name);
        }

        [TestMethod]
        public void CreateWithNameAndValue()
        {
            Machine machine = new Machine();
            Var var = new Var(machine, "one", 1);

            Assert.AreEqual("one", var.Name);
            Assert.AreEqual("user/one", var.FullName);
            Assert.AreEqual(1, var.Value);
            Assert.IsNull(var.Metadata);
        }

        [TestMethod]
        public void WithMetadata()
        {
            Machine machine = new Machine();
            Var var = new Var(machine, "one", 1);
            Map map = new Map(new object[] { });
            Var var2 = (Var)var.WithMetadata(map);

            Assert.IsNull(var.Metadata);
            Assert.IsNotNull(var2.Metadata);
            Assert.AreSame(map, var2.Metadata);
            Assert.AreEqual(1, var.Value);
            Assert.AreEqual(1, var2.Value);
            Assert.AreEqual("one", var2.Name);
            Assert.AreEqual("user/one", var2.FullName);
        }

        [TestMethod]
        public void IsMacro()
        {
            Machine machine = new Machine();
            Var var = new Var(machine, "one", 1);
            Map map = new Map(new object[] { new Keyword("macro"), true });
            Var var2 = (Var)var.WithMetadata(map);

            Assert.IsFalse(var.IsMacro);
            Assert.IsTrue(var2.IsMacro);
        }
    }
}
