namespace ClojSharp.Core.Tests.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using ClojSharp.Core.SpecialForms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NsTests
    {
        [TestMethod]
        public void NsSimpleName()
        {
            Machine machine = new Machine();
            IContext context = new VarContext(machine);
            Symbol symbol = new Symbol("foo");
            Ns ns = new Ns();

            var result = ns.Evaluate(context, new object[] { symbol });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Namespace));
            Assert.AreEqual("foo", ((Namespace)result).Name);

            var newns = context.GetValue("*ns*");
            Assert.IsNotNull(newns);
            Assert.AreSame(result, newns);
        }
    }
}
