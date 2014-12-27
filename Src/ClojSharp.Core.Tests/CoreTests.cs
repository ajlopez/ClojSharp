namespace ClojSharp.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Compiler;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using ClojSharp.Core.SpecialForms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [DeploymentItem("Src", "Src")]
    public class CoreTests
    {
        private Machine machine;

        [TestInitialize]
        public void Setup()
        {
            this.machine = new Machine();
            this.machine.RootContext.SetValue("seq?", null);
            this.machine.RootContext.SetValue("char?", null);
            this.machine.RootContext.SetValue("meta", null);
            this.machine.EvaluateFile("Src\\core.clj");
        }

        [TestMethod]
        public void MachineHasMacros()
        {
            this.IsMacro("defmacro");
            this.IsMacro("defn");
        }

        [TestMethod]
        public void MachineHasForms()
        {
            this.IsForm("second");
            this.IsForm("ffirst");
            this.IsForm("nfirst");
            this.IsForm("fnext");
            this.IsForm("nnext");
            this.IsForm("seq?");
            this.IsForm("char?");
            this.IsForm("map?");
            this.IsForm("vector?");
        }

        [TestMethod]
        public void DefineAndEvaluateFunction()
        {
            this.Evaluate("(defn incr [x] (+ x 1))");
            Assert.AreEqual(2, this.Evaluate("(incr 1)"));
        }

        [TestMethod]
        public void EvaluateSecond()
        {
            Assert.AreEqual(2, this.Evaluate("(second '(1 2 3))"));
        }

        [TestMethod]
        public void EvaluateFFirst()
        {
            Assert.AreEqual(1, this.Evaluate("(ffirst '((1 2 3) (4 5 6)))"));
        }

        [TestMethod]
        public void EvaluateNFirst()
        {
            Assert.AreEqual("(2 3)", this.Evaluate("(nfirst '((1 2 3) (4 5 6)))").ToString());
        }

        [TestMethod]
        public void EvaluateFNext()
        {
            Assert.AreEqual("(4 5 6)", this.Evaluate("(fnext '((1 2 3) (4 5 6)))").ToString());
        }

        [TestMethod]
        public void EvaluateNNext()
        {
            Assert.IsNull(this.Evaluate("(nnext '((1 2 3) (4 5 6)))"));
        }

        [TestMethod]
        public void EvaluateSeqP()
        {
            Assert.AreEqual(true, this.Evaluate("(seq? '(1 2 3))"));
            Assert.AreEqual(false, this.Evaluate("(seq? 42)"));
        }

        [TestMethod]
        public void EvaluateCharP()
        {
            Assert.AreEqual(true, this.Evaluate("(char? \\a)"));
            Assert.AreEqual(false, this.Evaluate("(char? 42)"));
            Assert.AreEqual(false, this.Evaluate("(char? \"foo\")"));
        }

        [TestMethod]
        public void EvaluateMapP()
        {
            Assert.AreEqual(true, this.Evaluate("(map? {:a 1 :b 2})"));
            Assert.AreEqual(false, this.Evaluate("(map? 42)"));
            Assert.AreEqual(false, this.Evaluate("(map? \"foo\")"));
        }

        [TestMethod]
        public void EvaluateVectorP()
        {
            Assert.AreEqual(true, this.Evaluate("(vector? [1 2])"));
            Assert.AreEqual(false, this.Evaluate("(vector? 42)"));
            Assert.AreEqual(false, this.Evaluate("(vector? \"foo\")"));
        }

        [TestMethod]
        public void EvaluateMeta()
        {
            this.Evaluate("(def one 1)");

            var result = this.Evaluate("(meta (var one))");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Map));

            var metadata = (Map)result;

            Assert.AreEqual("one", metadata.GetValue("name"));
            Assert.IsNotNull(metadata.GetValue("ns"));
            Assert.IsInstanceOfType(metadata.GetValue("ns"), typeof(Namespace));
            Assert.AreEqual("user", ((Namespace)metadata.GetValue("ns")).Name);
        }

        [TestMethod]
        public void EvaluateWithMeta()
        {
            var result = this.Evaluate("(with-meta [1 2 3] { :my \"meta\" })");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Vector));

            var vector = (Vector)result;
            var metadata = vector.Metadata;

            Assert.IsNotNull(metadata);

            Assert.AreEqual("meta", metadata.GetValue(new Keyword("my")));
        }

        [TestMethod]
        public void EvaluateLastInList()
        {
            Assert.AreEqual(3, this.Evaluate("(last '(1 2 3))"));
            Assert.AreEqual(1, this.Evaluate("(last '(1))"));
            Assert.IsNull(this.Evaluate("(last '())"));
            Assert.IsNull(this.Evaluate("(last nil)"));
        }

        [TestMethod]
        public void EvaluateLastInVector()
        {
            Assert.AreEqual(3, this.Evaluate("(last [1 2 3])"));
            Assert.AreEqual(1, this.Evaluate("(last [1])"));
            Assert.IsNull(this.Evaluate("(last [])"));
            Assert.IsNull(this.Evaluate("(last nil)"));
        }

        [TestMethod]
        public void EvaluateButLastInList()
        {
            Assert.AreEqual("(1 2)", this.Evaluate("(butlast '(1 2 3))").ToString());
            Assert.IsNull(this.Evaluate("(butlast '(1))"));
        }

        [TestMethod]
        public void EvaluateButLastInVector()
        {
            Assert.AreEqual("(1 2)", this.Evaluate("(butlast [1 2 3])").ToString());
            Assert.IsNull(this.Evaluate("(butlast [1])"));
        }

        [TestMethod]
        public void EvaluateIfNot()
        {
            Assert.AreEqual(1, this.Evaluate("(if-not false 1)"));
            Assert.IsNull(this.Evaluate("(if-not true 2)"));
            Assert.AreEqual(1, this.Evaluate("(if-not false 1 2)"));
            Assert.AreEqual(2, this.Evaluate("(if-not true 1 2)"));
        }

        [TestMethod]
        public void EvaluateWhen()
        {
            Assert.AreEqual(3, this.Evaluate("(when true 1 2 3)"));
            Assert.IsNull(this.Evaluate("(when false 1 2 3)"));
        }

        private void IsForm(string name)
        {
            var result = this.machine.RootContext.GetValue(name);
            Assert.IsNotNull(result, name);
            Assert.IsInstanceOfType(result, typeof(IForm), name);
            Assert.IsInstanceOfType(result, typeof(BaseForm), name);
        }

        private void IsMacro(string name)
        {
            var result = this.machine.RootContext.GetValue(name);
            Assert.IsNotNull(result, name);
            Assert.IsInstanceOfType(result, typeof(IForm), name);
            Assert.IsInstanceOfType(result, typeof(Macro), name);
        }

        private object Evaluate(string text)
        {
            return this.Evaluate(text, this.machine.RootContext);
        }

        private object Evaluate(string text, IContext context)
        {
            Parser parser = new Parser(text);
            var expr = parser.ParseExpression();
            Assert.IsNull(parser.ParseExpression());
            return Machine.Evaluate(expr, context);
        }
    }
}
