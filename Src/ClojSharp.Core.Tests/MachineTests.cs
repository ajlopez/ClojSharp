namespace ClojSharp.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MachineTests
    {
        private Machine machine;

        [TestInitialize]
        public void Setup()
        {
            this.machine = new Machine();
        }

        [TestMethod]
        public void MachineHasRootContext()
        {
            Assert.IsNotNull(this.machine.RootContext);
        }

        [TestMethod]
        public void InitialNamespace()
        {
            var result = this.machine.RootContext.GetValue("*ns*");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Namespace));

            var ns = (Namespace)result;

            Assert.AreEqual("clojsharp.core", ns.Name);
        }

        [TestMethod]
        public void MachineHasDefinedForms()
        {
            this.IsForm("+");
            this.IsForm("-");
            this.IsForm("*");
            this.IsForm("/");
            this.IsForm("mod");
            this.IsForm("rem");

            this.IsForm("cons");
            this.IsForm("list");
            this.IsForm("first");
            this.IsForm("rest");
            this.IsForm("next");

            this.IsForm("not");
            this.IsForm("nil?");
            this.IsForm("number?");
            this.IsForm("false?");
            this.IsForm("true?");
            this.IsForm("zero?");
            this.IsForm("integer?");
            this.IsForm("float?");
            this.IsForm("char?");
            this.IsForm("symbol?");
            this.IsForm("seq?");
            this.IsForm("blank?");
            this.IsForm("instance?");

            this.IsForm("str");
            this.IsForm("rand");
            this.IsForm("atom");
            this.IsForm("deref");

            this.IsForm("meta");
            this.IsForm("class");

            this.IsForm("max");
            this.IsForm("min");

            this.IsForm("seq");
        }

        [TestMethod]
        public void MachineHasDefinedSpecialForms()
        {
            this.IsSpecialForm("def");
            this.IsSpecialForm("fn");
            this.IsSpecialForm("mfn");
            this.IsSpecialForm("quote");
            this.IsSpecialForm("backquote");
            this.IsSpecialForm("let");
            this.IsSpecialForm("if");
            this.IsSpecialForm("do");
            this.IsSpecialForm("var");
            this.IsSpecialForm("ns");

            this.IsSpecialForm("and");
            this.IsSpecialForm("or");

            this.IsSpecialForm("new");

            this.IsSpecialForm(".");
        }

        [TestMethod]
        public void MachineEvaluateObjects()
        {
            Assert.AreEqual(3, Machine.Evaluate(3, null));
            Assert.AreEqual("foo", Machine.Evaluate("foo", null));
            Assert.IsNull(Machine.Evaluate(null, null));
        }

        [TestMethod]
        public void MachineEvaluateSymbolInContext()
        {
            Context context = new Context();
            context.SetValue("one", 1);

            Assert.AreEqual(1, Machine.Evaluate(new Symbol("one"), context));
        }

        [TestMethod]
        public void MachineValueToString()
        {
            Assert.AreEqual("foo", Machine.ToString("foo"));
            Assert.AreEqual("42", Machine.ToString(42));
            Assert.AreEqual("nil", Machine.ToString(null));
            Assert.AreEqual("false", Machine.ToString(false));
            Assert.AreEqual("true", Machine.ToString(true));
        }

        [TestMethod]
        public void MachineCharToString()
        {
            Assert.AreEqual("\\a", Machine.ToString('a'));
            Assert.AreEqual("\\C", Machine.ToString('C'));
            Assert.AreEqual("\\\\n", Machine.ToString('\n'));
            Assert.AreEqual("\\\\r", Machine.ToString('\r'));
            Assert.AreEqual("\\\\t", Machine.ToString('\t'));
        }

        [TestMethod]
        public void GetUnknownNamespaceAsNull()
        {
            Assert.IsNull(machine.GetNamespace("foo"));
        }

        [TestMethod]
        public void SetAndGetNamespace()
        {
            var ns = new Namespace("ns1");
            machine.SetNamespace(ns);

            Assert.AreSame(ns, machine.GetNamespace("ns1"));
        }

        private void IsSpecialForm(string name)
        {
            var result = this.machine.RootContext.GetValue(name);
            Assert.IsNotNull(result, name);
            Assert.IsInstanceOfType(result, typeof(IForm), name);
            Assert.IsNotInstanceOfType(result, typeof(BaseForm), name);
        }

        private void IsForm(string name)
        {
            var result = this.machine.RootContext.GetValue(name);
            Assert.IsNotNull(result, name);
            Assert.IsInstanceOfType(result, typeof(IForm), name);
            Assert.IsInstanceOfType(result, typeof(BaseForm), name);
        }
    }
}
