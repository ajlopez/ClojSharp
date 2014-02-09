namespace ClojSharp.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Compiler;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EvaluateTests
    {
        private Machine machine;

        [TestInitialize]
        public void Setup()
        {
            this.machine = new Machine();
        }

        [TestMethod]
        public void EvaluateInteger()
        {
            Assert.AreEqual(123, this.Evaluate("123", null));
        }

        [TestMethod]
        public void EvaluateSymbolInContext()
        {
            Context context = new Context();
            context.SetValue("one", 1);
            Assert.AreEqual(1, this.Evaluate("one", context));
        }

        [TestMethod]
        public void EvaluateAdd()
        {
            Assert.AreEqual(3, this.Evaluate("(+ 1 2)"));
            Assert.AreEqual(6, this.Evaluate("(+ 1 2 3)"));
            Assert.AreEqual(5, this.Evaluate("(+ 5)"));
            Assert.AreEqual(0, this.Evaluate("(+)"));
        }

        [TestMethod]
        public void EvaluateSubtract()
        {
            Assert.AreEqual(-1, this.Evaluate("(- 1 2)"));
            Assert.AreEqual(-4, this.Evaluate("(- 1 2 3)"));
            Assert.AreEqual(-5, this.Evaluate("(- 5)"));
        }

        [TestMethod]
        public void EvaluateMultiply()
        {
            Assert.AreEqual(1, this.Evaluate("(*)"));
            Assert.AreEqual(2, this.Evaluate("(* 2)"));
            Assert.AreEqual(6, this.Evaluate("(* 2 3)"));
            Assert.AreEqual(24, this.Evaluate("(* 2 3 4)"));
        }

        [TestMethod]
        public void EvaluateDivide()
        {
            Assert.AreEqual(1, this.Evaluate("(/ 1)"));
            Assert.AreEqual(2, this.Evaluate("(/ 4 2)"));
            Assert.AreEqual(2, this.Evaluate("(/ 8 2 2)"));
        }

        [TestMethod]
        public void EvaluateSimpleFn()
        {
            Assert.AreEqual(1, this.Evaluate("((fn [] 1))"));
        }

        [TestMethod]
        public void EvaluateFnWithSimpleBody()
        {
            Assert.AreEqual(3, this.Evaluate("((fn [] (+ 1 2)))"));
        }

        [TestMethod]
        public void EvaluateFnWithArgument()
        {
            Assert.AreEqual(3, this.Evaluate("((fn [x] (+ 1 x)) 2)"));
        }

        [TestMethod]
        public void EvaluateFnWithVariableArity()
        {
            Assert.AreEqual("(1 2 3)", this.Evaluate("((fn [& rest] rest) 1 2 3)").ToString());
        }

        [TestMethod]
        public void EvaluateFnWithArgumentsAndVariableArity()
        {
            Assert.AreEqual("(3 4)", this.Evaluate("((fn [x y & rest] rest) 1 2 3 4)").ToString());
        }

        [TestMethod]
        public void EvaluateFnWithArgumentAndFreeVariable()
        {
            this.machine.RootContext.SetValue("one", 1);
            Assert.AreEqual(3, this.Evaluate("((fn [x] (+ one x)) 2)"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseWhenAdditionalFirstArgument()
        {
            Assert.AreEqual(1, this.Evaluate("((fn [] 1) 2)"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseWhenAdditionalSecondArgument()
        {
            Assert.AreEqual(1, this.Evaluate("((fn [x] (+ 1 x)) 2 3)"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseWhenMissingFirstArgument()
        {
            Assert.AreEqual(1, this.Evaluate("((fn [x] (+ 1 x)))"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArityException))]
        public void RaiseArityExceptionWhenEvaluateDivideWithoutArguments()
        {
            Assert.AreEqual(1, this.Evaluate("(/)"));
        }

        [TestMethod]
        public void EvaluateQuoteInteger()
        {
            Assert.AreEqual(1, this.Evaluate("(quote 1)"));
        }

        [TestMethod]
        public void EvaluateQuoteList()
        {
            Assert.AreEqual("(1 2)", this.Evaluate("(quote (1 2))").ToString());
            Assert.AreEqual("(1 2)", this.Evaluate("'(1 2)").ToString());
        }

        [TestMethod]
        public void EvaluateList()
        {
            Assert.AreEqual("(1 2)", this.Evaluate("(list 1 2)").ToString());
            Assert.AreEqual("(1 2 3)", this.Evaluate("(list 1 2 3)").ToString());
            Assert.AreEqual("()", this.Evaluate("(list)").ToString());
            Assert.AreEqual("(nil)", this.Evaluate("(list nil)").ToString());
        }

        [TestMethod]
        public void EvaluateFirst()
        {
            Assert.AreEqual(1, this.Evaluate("(first (list 1 2))"));
            Assert.IsNull(this.Evaluate("(first nil)"));
            Assert.IsNull(this.Evaluate("(first ())"));
        }

        [TestMethod]
        public void EvaluateRest()
        {
            Assert.AreEqual("(2)", this.Evaluate("(rest (list 1 2))").ToString());
            Assert.AreSame(EmptyList.Instance, this.Evaluate("(rest nil)"));
            Assert.AreSame(EmptyList.Instance, this.Evaluate("(rest ())"));
        }

        [TestMethod]
        public void EvaluateNext()
        {
            Assert.AreEqual("(2)", this.Evaluate("(next (list 1 2))").ToString());
            Assert.IsNull(this.Evaluate("(next nil)"));
            Assert.IsNull(this.Evaluate("(next ())"));
        }

        [TestMethod]
        public void EvaluateVectorWithForms()
        {
            var result = this.Evaluate("[(+ 1 2) (+ 2 3) (+ 3 4)]");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Vector));

            var vector = (Vector)result;

            Assert.AreEqual(3, vector.Elements.Count);
            Assert.AreEqual(3, vector.Elements[0]);
            Assert.AreEqual(5, vector.Elements[1]);
            Assert.AreEqual(7, vector.Elements[2]);
        }

        [TestMethod]
        public void EvaluateMapWithForms()
        {
            var result = this.Evaluate("{:three (+ 1 2) :five (+ 2 3) :seven (+ 3 4)}");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Map));

            var map = (Map)result;

            Assert.AreEqual(3, map.GetValue(new Keyword("three")));
            Assert.AreEqual(5, map.GetValue(new Keyword("five")));
            Assert.AreEqual(7, map.GetValue(new Keyword("seven")));
        }

        [TestMethod]
        public void EvaluateLet()
        {
            var result = this.Evaluate("(let [x 1 y (+ x 1)] y)");

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void EvaluateIfNil()
        {
            var result = this.Evaluate("(if nil 1 2)");

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void EvaluateIfTrue()
        {
            var result = this.Evaluate("(if true 1 2)");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void EvaluateIfOneWithoutElse()
        {
            var result = this.Evaluate("(if 1 2)");

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void EvaluateIfFalseWithoutElse()
        {
            var result = this.Evaluate("(if false 1)");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ExceptionWhenOddNumberOfForms()
        {
            try
            {
                this.Evaluate("{1 2 3}");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(RuntimeException));
                Assert.AreEqual("Map literal must contain an even number of forms", ex.Message);
            }
        }

        [TestMethod]
        public void EvaluateDo()
        {
            var result = this.Evaluate("(do 1 2 3)");

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void EvaluateDefinedVar()
        {
            var machine = new Machine();
            this.Evaluate("(def one 1)", machine.RootContext);
            var result = this.Evaluate("(var one)", machine.RootContext);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Var));

            var var = (Var)result;
            Assert.AreEqual("one", var.Name);
            Assert.AreEqual("user/one", var.FullName);
            Assert.AreEqual(1, var.Value);
        }

        [TestMethod]
        public void RaiseWhenEvaluateUndefinedVar()
        {
            var machine = new Machine();

            try
            {
                this.Evaluate("(var one)", machine.RootContext);
                Assert.Fail();
            }
            catch (RuntimeException ex)
            {
                Assert.AreEqual("Unable to resolve var: one in this context", ex.Message);
            }
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
