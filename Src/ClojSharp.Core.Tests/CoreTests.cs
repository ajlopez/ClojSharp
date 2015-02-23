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
        public void EvaluateSetP()
        {
            Assert.AreEqual(true, this.Evaluate("(set? #{:a :b})"));
            Assert.AreEqual(false, this.Evaluate("(set? {:a 1 :b 2})"));
            Assert.AreEqual(false, this.Evaluate("(set? 42)"));
            Assert.AreEqual(false, this.Evaluate("(set? \"foo\")"));
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

        [TestMethod]
        public void EvaluateWhenNot()
        {
            Assert.AreEqual(3, this.Evaluate("(when-not false 1 2 3)"));
            Assert.IsNull(this.Evaluate("(when-not true 1 2 3)"));
        }

        [TestMethod]
        public void EvaluateVector()
        {
            Assert.AreEqual("[]", this.Evaluate("(vector)").ToString());
            Assert.AreEqual("[1]", this.Evaluate("(vector 1)").ToString());
            Assert.AreEqual("[1 2]", this.Evaluate("(vector 1 2)").ToString());
            Assert.AreEqual("[1 2 3]", this.Evaluate("(vector 1 2 3)").ToString());
            Assert.AreEqual("[1 2 3 4]", this.Evaluate("(vector 1 2 3 4)").ToString());
            Assert.AreEqual("[1 2 3 4 5]", this.Evaluate("(vector 1 2 3 4 5)").ToString());
            Assert.AreEqual("[1 2 3 4 5 6]", this.Evaluate("(vector 1 2 3 4 5 6)").ToString());
        }

        [TestMethod]
        public void EvaluateEmptyHashMap()
        {
            var result = this.Evaluate("(hash-map)");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Map));

            var map = (Map)result;

            Assert.IsNull(map.GetValue("name"));
        }

        [TestMethod]
        public void EvaluateHashMap()
        {
            var result = this.Evaluate("(hash-map :one 1 :two 2 :three 3)");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Map));

            var map = (Map)result;

            Assert.AreEqual(1, map.GetValue(new Keyword("one")));
            Assert.AreEqual(2, map.GetValue(new Keyword("two")));
            Assert.AreEqual(3, map.GetValue(new Keyword("three")));
        }

        [TestMethod]
        public void EvaluateAssocOnMap()
        {
            var result = this.Evaluate("(assoc { :one 0 :two 2 } :one 1 :three 3)");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Map));

            var map = (Map)result;

            Assert.AreEqual(1, map.GetValue(new Keyword("one")));
            Assert.AreEqual(2, map.GetValue(new Keyword("two")));
            Assert.AreEqual(3, map.GetValue(new Keyword("three")));
        }

        [TestMethod]
        public void EvaluateDissocOnMap()
        {
            var result = this.Evaluate("(dissoc { :one 1 :two 2 :three 3 :four 4 } :two :four)");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Map));

            var map = (Map)result;

            Assert.AreEqual(1, map.GetValue(new Keyword("one")));
            Assert.IsNull(map.GetValue(new Keyword("two")));
            Assert.AreEqual(3, map.GetValue(new Keyword("three")));
            Assert.IsNull(map.GetValue(new Keyword("four")));
        }

        [TestMethod]
        public void EvaluateDissocOnSet()
        {
            var result = this.Evaluate("(dissoc #{ :one :two :three :four } :two :four)");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Set));

            var set = (Set)result;

            Assert.IsTrue(set.HasKey(new Keyword("one")));
            Assert.IsFalse(set.HasKey(new Keyword("two")));
            Assert.IsTrue(set.HasKey(new Keyword("three")));
            Assert.IsFalse(set.HasKey(new Keyword("four")));
        }

        [TestMethod]
        public void EvaluateAssocOnVector()
        {
            var result = this.Evaluate("(assoc [1 2 3] 1 3 3 4)");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Vector));

            var vector = (Vector)result;

            Assert.AreEqual(4, vector.Elements.Count);
            Assert.AreEqual(1, vector.Elements[0]);
            Assert.AreEqual(3, vector.Elements[1]);
            Assert.AreEqual(3, vector.Elements[2]);
            Assert.AreEqual(4, vector.Elements[3]);
        }

        [TestMethod]
        public void EvaluateCond()
        {
            Assert.AreEqual("true", this.Evaluate("(cond (true? true) \"true\" (false? true) \"false\" :else \"other\")"));
            Assert.AreEqual("false", this.Evaluate("(cond (true? false) \"true\" (false? false) \"false\" :else \"other\")"));
            Assert.AreEqual("other", this.Evaluate("(cond (true? 0) \"true\" (false? 0) \"false\" :else \"other\")"));
        }

        [TestMethod]
        public void EvaluateCount()
        {
            Assert.AreEqual(3, this.Evaluate("(count [1 2 3])"));
            Assert.AreEqual(0, this.Evaluate("(count '())"));
            Assert.AreEqual(2, this.Evaluate("(count '(1 2))"));
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
