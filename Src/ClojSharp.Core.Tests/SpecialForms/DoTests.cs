namespace ClojSharp.Core.Tests.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.SpecialForms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClojSharp.Core.Language;

    [TestClass]
    public class DoTests
    {
        [TestMethod]
        public void DoIntegers()
        {
            Do @do = new Do();

            var result = @do.Evaluate(null, new object[] { 1, 2, 3 });

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void DoReturnsNullIfNoArguments()
        {
            Do @do = new Do();

            Assert.IsNull(@do.Evaluate(null, new object[] { }));
        }

        [TestMethod]
        public void DoDefines()
        {
            var def1 = new List(new Symbol("def"), new List(new Symbol("one"), new List(1, null)));
            var def2 = new List(new Symbol("def"), new List(new Symbol("two"), new List(2, null)));
            var def3 = new List(new Symbol("def"), new List(new Symbol("three"), new List(3, null)));
            var machine = new Machine();

            Do @do = new Do();

            var result = @do.Evaluate(machine.RootContext, new object[] { def1, def2, def3 });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Var));

            var @var = (Var)result;

            Assert.AreEqual("three", @var.Name);

            Assert.AreEqual(1, machine.RootContext.GetValue("one"));
            Assert.AreEqual(2, machine.RootContext.GetValue("two"));
            Assert.AreEqual(3, machine.RootContext.GetValue("three"));
        }
    }
}
