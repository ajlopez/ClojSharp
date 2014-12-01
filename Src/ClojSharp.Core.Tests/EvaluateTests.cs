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
        public void EvaluateNegativeInteger()
        {
            Assert.AreEqual(-123, this.Evaluate("-123", null));
        }

        [TestMethod]
        public void EvaluateString()
        {
            Assert.AreEqual(string.Empty, this.Evaluate("\"\"", null));
            Assert.AreEqual("foo", this.Evaluate("\"foo\"", null));
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
            Assert.AreEqual(3.2, this.Evaluate("(+ 1.2 2)"));
            Assert.AreEqual(3.4, this.Evaluate("(+ 1 2.4)"));
            Assert.AreEqual(3.5, this.Evaluate("(+ 1.1 2.4)"));
        }

        [TestMethod]
        public void EvaluateSubtract()
        {
            Assert.AreEqual(-1, this.Evaluate("(- 1 2)"));
            Assert.AreEqual(-4, this.Evaluate("(- 1 2 3)"));
            Assert.AreEqual(-5, this.Evaluate("(- 5)"));
            Assert.AreEqual(-5.6, this.Evaluate("(- 5.6)"));
            Assert.AreEqual(1.2 - 2, this.Evaluate("(- 1.2 2)"));
            Assert.AreEqual(1 - 2.4, this.Evaluate("(- 1 2.4)"));
            Assert.AreEqual(1.1 - 2.4, this.Evaluate("(- 1.1 2.4)"));
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
        public void EvaluateMod()
        {
            Assert.AreEqual(1, this.Evaluate("(mod 10 3)"));
            Assert.AreEqual(2, this.Evaluate("(mod -10 3)"));
            Assert.AreEqual(1, this.Evaluate("(mod 5 2)"));
        }

        [TestMethod]
        public void EvaluateRem()
        {
            Assert.AreEqual(1, this.Evaluate("(rem 10 3)"));
            Assert.AreEqual(-1, this.Evaluate("(rem -10 3)"));
            Assert.AreEqual(1, this.Evaluate("(rem 5 2)"));
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
        public void EvaluateFnWithMultiFunction()
        {
            Assert.AreEqual(3, this.Evaluate("((fn ([x] (+ 1 x)) ([x y] (+ x y))) 2)"));
            Assert.AreEqual(5, this.Evaluate("((fn ([x] (+ 1 x)) ([x y] (+ x y))) 2 3)"));
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
        public void EvaluateQuoteVector()
        {
            var result = this.Evaluate("(quote [1 2 3])");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Vector));
            Assert.AreEqual("[1 2 3]", result.ToString());
        }

        [TestMethod]
        public void EvaluateQuoteVectorWithList()
        {
            var result = this.Evaluate("(quote [1 (+ 1 1) 3])");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Vector));
            Assert.AreEqual("[1 (+ 1 1) 3]", result.ToString());
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
        public void EvaluateDefinedDefValue()
        {
            var machine = new Machine();
            this.Evaluate("(def one 1)", machine.RootContext);
            var result = this.Evaluate("one", machine.RootContext);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void EvaluateDefinedDefFunction()
        {
            var machine = new Machine();
            this.Evaluate("(def inc (fn [x] (+ x 1)))", machine.RootContext);
            var result = this.Evaluate("(inc 1)", machine.RootContext);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void EvaluateDefinedDefMacro()
        {
            var machine = new Machine();
            this.Evaluate("(def mylist (mfn [x] (cons 'list (cons x nil))))", machine.RootContext);
            var result = this.Evaluate("(mylist 1)", machine.RootContext);

            Assert.IsNotNull(result);
            Assert.AreEqual("(1)", result.ToString());
        }

        [TestMethod]
        public void EvaluateDefinedDefMacroUsingBackquote()
        {
            var machine = new Machine();
            this.Evaluate("(def mylist (mfn [x] `(list ~x)))", machine.RootContext);
            var result = this.Evaluate("(mylist 1)", machine.RootContext);

            Assert.IsNotNull(result);
            Assert.AreEqual("(1)", result.ToString());
        }

        [TestMethod]
        public void EvaluateBackquoteOverListWithUnquote()
        {
            var machine = new Machine();
            var result = this.Evaluate("(let [x 2] `(1 ~x 3))", machine.RootContext);

            Assert.IsNotNull(result);
            Assert.AreEqual("(1 2 3)", result.ToString());
        }

        [TestMethod]
        public void EvaluateBackquoteOverListWithUnquoteSplice()
        {
            var machine = new Machine();
            var result = this.Evaluate("(let [x '(2 3)] `(1 ~@x 4))", machine.RootContext);

            Assert.IsNotNull(result);
            Assert.AreEqual("(1 2 3 4)", result.ToString());
        }

        [TestMethod]
        public void EvaluateBackquoteOverListWithUnquoteSpliceAndUnquote()
        {
            var machine = new Machine();
            var result = this.Evaluate("(let [x '(2 3) y 4] `(1 ~@x ~y))", machine.RootContext);

            Assert.IsNotNull(result);
            Assert.AreEqual("(1 2 3 4)", result.ToString());
        }

        [TestMethod]
        public void EvaluateBackquoteOverVector()
        {
            var machine = new Machine();
            var result = this.Evaluate("(let [x 2] `[1 ~x 3])", machine.RootContext);

            Assert.IsNotNull(result);
            Assert.AreEqual("[1 2 3]", result.ToString());
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
        public void EvaluateDefinedVarWithMetadata()
        {
            var machine = new Machine();
            this.Evaluate("(def ^{:m true} one 1)", machine.RootContext);
            var result = this.Evaluate("(meta (var one))", machine.RootContext);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Map));

            var map = (Map)result;
            Assert.AreEqual(true, map.GetValue(new Keyword("m")));
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

        [TestMethod]
        public void EvaluateOr()
        {
            Assert.AreEqual(null, this.Evaluate("(or)"));
            Assert.AreEqual(true, this.Evaluate("(or true)"));
            Assert.AreEqual(true, this.Evaluate("(or true (bad))"));
            Assert.AreEqual(true, this.Evaluate("(or true true)"));
            Assert.AreEqual(1, this.Evaluate("(or 1 true)"));
            Assert.AreEqual(1, this.Evaluate("(or 1 (bad))"));
            Assert.AreEqual(true, this.Evaluate("(or true 1)"));
            Assert.AreEqual(1, this.Evaluate("(or false 1)"));
            Assert.AreEqual(1, this.Evaluate("(or false 1 2)"));
            Assert.AreEqual(1, this.Evaluate("(or false 1 2)"));

            Assert.AreEqual(false, this.Evaluate("(or false)"));
            Assert.AreEqual(null, this.Evaluate("(or nil)"));
            Assert.AreEqual(true, this.Evaluate("(or true nil)"));
            Assert.AreEqual(true, this.Evaluate("(or true false)"));
        }

        [TestMethod]
        public void EvaluateAnd()
        {
            Assert.AreEqual(true, this.Evaluate("(and)"));
            Assert.AreEqual(true, this.Evaluate("(and true)"));
            Assert.AreEqual(true, this.Evaluate("(and true true)"));
            Assert.AreEqual(1, this.Evaluate("(and true 1)"));

            Assert.AreEqual(false, this.Evaluate("(and false)"));
            Assert.AreEqual(false, this.Evaluate("(and false (bad))"));
            Assert.AreEqual(null, this.Evaluate("(and nil)"));
            Assert.AreEqual(null, this.Evaluate("(and nil (bad))"));
            Assert.AreEqual(null, this.Evaluate("(and true nil)"));
            Assert.AreEqual(false, this.Evaluate("(and true false)"));
        }

        [TestMethod]
        public void EvaluateNot()
        {
            Assert.AreEqual(true, this.Evaluate("(not false)"));
            Assert.AreEqual(true, this.Evaluate("(not nil)"));

            Assert.AreEqual(false, this.Evaluate("(not 1)"));
            Assert.AreEqual(false, this.Evaluate("(not 0)"));
            Assert.AreEqual(false, this.Evaluate("(not \"\")"));
            Assert.AreEqual(false, this.Evaluate("(not \"foo\")"));
        }

        [TestMethod]
        public void EvaluateNilP()
        {
            Assert.AreEqual(true, this.Evaluate("(nil? nil)"));

            Assert.AreEqual(false, this.Evaluate("(nil? false)"));
            Assert.AreEqual(false, this.Evaluate("(nil? true)"));
            Assert.AreEqual(false, this.Evaluate("(nil? 0)"));
            Assert.AreEqual(false, this.Evaluate("(nil? 1)"));
            Assert.AreEqual(false, this.Evaluate("(nil? \"\")"));
            Assert.AreEqual(false, this.Evaluate("(nil? \"foo\")"));
        }

        [TestMethod]
        public void EvaluateFalseP()
        {
            Assert.AreEqual(true, this.Evaluate("(false? false)"));

            Assert.AreEqual(false, this.Evaluate("(false? nil)"));
            Assert.AreEqual(false, this.Evaluate("(false? true)"));
            Assert.AreEqual(false, this.Evaluate("(false? 0)"));
            Assert.AreEqual(false, this.Evaluate("(false? 1)"));
            Assert.AreEqual(false, this.Evaluate("(false? \"\")"));
            Assert.AreEqual(false, this.Evaluate("(false? \"foo\")"));
        }

        [TestMethod]
        public void EvaluateZeroP()
        {
            Assert.AreEqual(true, this.Evaluate("(zero? 0)"));
            Assert.AreEqual(true, this.Evaluate("(zero? 0.0)"));

            Assert.AreEqual(false, this.Evaluate("(zero? nil)"));
            Assert.AreEqual(false, this.Evaluate("(zero? true)"));
            Assert.AreEqual(false, this.Evaluate("(zero? 0.1)"));
            Assert.AreEqual(false, this.Evaluate("(zero? 1)"));
            Assert.AreEqual(false, this.Evaluate("(zero? \"\")"));
            Assert.AreEqual(false, this.Evaluate("(zero? \"foo\")"));
        }

        [TestMethod]
        public void EvaluateIntegerP()
        {
            Assert.AreEqual(true, this.Evaluate("(integer? 0)"));
            Assert.AreEqual(true, this.Evaluate("(integer? 42)"));

            Assert.AreEqual(false, this.Evaluate("(integer? nil)"));
            Assert.AreEqual(false, this.Evaluate("(integer? true)"));
            Assert.AreEqual(false, this.Evaluate("(integer? 0.1)"));
            Assert.AreEqual(false, this.Evaluate("(integer? \"\")"));
            Assert.AreEqual(false, this.Evaluate("(integer? \"foo\")"));
        }

        [TestMethod]
        public void EvaluateFloatP()
        {
            Assert.AreEqual(true, this.Evaluate("(float? 12.34)"));
            Assert.AreEqual(true, this.Evaluate("(float? 0.0)"));

            Assert.AreEqual(false, this.Evaluate("(float? nil)"));
            Assert.AreEqual(false, this.Evaluate("(float? true)"));
            Assert.AreEqual(false, this.Evaluate("(float? 1)"));
            Assert.AreEqual(false, this.Evaluate("(float? \"\")"));
            Assert.AreEqual(false, this.Evaluate("(float? \"foo\")"));
        }

        [TestMethod]
        public void EvaluateCharP()
        {
            Assert.AreEqual(true, this.Evaluate("(char? \\a)"));

            Assert.AreEqual(false, this.Evaluate("(char? nil)"));
            Assert.AreEqual(false, this.Evaluate("(char? true)"));
            Assert.AreEqual(false, this.Evaluate("(char? 1)"));
            Assert.AreEqual(false, this.Evaluate("(char? \"\")"));
            Assert.AreEqual(false, this.Evaluate("(char? \"foo\")"));
        }

        [TestMethod]
        public void EvaluateTrueP()
        {
            Assert.AreEqual(true, this.Evaluate("(true? true)"));

            Assert.AreEqual(false, this.Evaluate("(true? nil)"));
            Assert.AreEqual(false, this.Evaluate("(true? false)"));
            Assert.AreEqual(false, this.Evaluate("(true? 0)"));
            Assert.AreEqual(false, this.Evaluate("(true? 1)"));
            Assert.AreEqual(false, this.Evaluate("(true? \"\")"));
            Assert.AreEqual(false, this.Evaluate("(true? \"foo\")"));
        }

        [TestMethod]
        public void EvaluateStringP()
        {
            Assert.AreEqual(true, this.Evaluate("(string? \"foo\")"));

            Assert.AreEqual(false, this.Evaluate("(string? \\a)"));
            Assert.AreEqual(false, this.Evaluate("(string? false)"));
            Assert.AreEqual(false, this.Evaluate("(string? 0)"));
            Assert.AreEqual(false, this.Evaluate("(string? 1)"));
        }

        [TestMethod]
        public void EvaluateStr()
        {
            Assert.AreEqual(string.Empty, this.Evaluate("(str)"));
            Assert.AreEqual(string.Empty, this.Evaluate("(str nil)"));
            Assert.AreEqual("1", this.Evaluate("(str 1)"));
            Assert.AreEqual("123", this.Evaluate("(str 1 2 3)"));
            Assert.AreEqual("1symbol:keyword", this.Evaluate("(str 1 'symbol :keyword)"));
        }

        [TestMethod]
        public void EvaluateNumberP()
        {
            Assert.AreEqual(true, this.Evaluate("(number? 1)"));
            Assert.AreEqual(true, this.Evaluate("(number? 1.0)"));
            Assert.AreEqual(false, this.Evaluate("(number? :a)"));
            Assert.AreEqual(false, this.Evaluate("(number? nil)"));
            Assert.AreEqual(false, this.Evaluate("(number? \"foo\")"));
        }

        [TestMethod]
        public void EvaluateSymbolP()
        {
            Assert.AreEqual(true, this.Evaluate("(symbol? 'a)"));
            Assert.AreEqual(false, this.Evaluate("(symbol? 1.0)"));
            Assert.AreEqual(false, this.Evaluate("(symbol? :a)"));
            Assert.AreEqual(false, this.Evaluate("(symbol? nil)"));
            Assert.AreEqual(false, this.Evaluate("(symbol? \"foo\")"));
        }

        [TestMethod]
        public void EvaluateSeqP()
        {
            Assert.AreEqual(true, this.Evaluate("(seq? '(a b))"));
            Assert.AreEqual(false, this.Evaluate("(seq? 1.0)"));
            Assert.AreEqual(false, this.Evaluate("(seq? :a)"));
            Assert.AreEqual(false, this.Evaluate("(seq? nil)"));
            Assert.AreEqual(false, this.Evaluate("(seq? \"foo\")"));
        }

        [TestMethod]
        public void EvaluateBlankP()
        {
            Assert.AreEqual(true, this.Evaluate("(blank? nil)"));
            Assert.AreEqual(true, this.Evaluate("(blank? \"\")"));
            Assert.AreEqual(true, this.Evaluate("(blank? \"  \")"));

            Assert.AreEqual(false, this.Evaluate("(blank? 1.0)"));
            Assert.AreEqual(false, this.Evaluate("(blank? :a)"));
            Assert.AreEqual(false, this.Evaluate("(blank? 42)"));
            Assert.AreEqual(false, this.Evaluate("(blank? \"foo\")"));
        }

        [TestMethod]
        public void EvaluateMax()
        {
            Assert.AreEqual(1, this.Evaluate("(max 1)"));
            Assert.AreEqual(2, this.Evaluate("(max 1 2)"));
            Assert.AreEqual(2, this.Evaluate("(max 2 1 2)"));
            Assert.AreEqual(3, this.Evaluate("(max 2 1 3 2)"));
        }

        [TestMethod]
        public void EvaluateMin()
        {
            Assert.AreEqual(1, this.Evaluate("(min 1)"));
            Assert.AreEqual(1, this.Evaluate("(min 1 2)"));
            Assert.AreEqual(1, this.Evaluate("(min 2 1 2)"));
            Assert.AreEqual(1, this.Evaluate("(min 2 1 3 2)"));
        }

        [TestMethod]
        public void EvaluateRand()
        {
            var result = this.Evaluate("(rand)");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(double));

            var value = (double)result;

            Assert.IsTrue(value >= 0);
            Assert.IsTrue(value < 1);
        }

        [TestMethod]
        public void EvaluateAtom()
        {
            var result = this.Evaluate("(atom 42)");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Atom));
            Assert.AreEqual(42, ((Atom)result).Value);
        }

        [TestMethod]
        public void EvaluateDerefAtom()
        {
            var result = this.Evaluate("(deref (atom 42))");

            Assert.IsNotNull(result);
            Assert.AreEqual(42, result);
        }

        [TestMethod]
        public void EvaluateDerefVar()
        {
            var machine = new Machine();
            this.Evaluate("(def one 1)", machine.RootContext);
            var result = this.Evaluate("(deref (var one))", machine.RootContext);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void EvaluateClass()
        {
            Assert.AreEqual(typeof(string), this.Evaluate("(class \"foo\")"));
            Assert.AreEqual(typeof(int), this.Evaluate("(class 42)"));
            Assert.AreEqual(typeof(ClojSharp.Core.Language.List), this.Evaluate("(class '(1 2))"));
            Assert.IsNull(this.Evaluate("(class nil)"));
        }

        [TestMethod]
        public void EvaluateRandWithInteger()
        {
            var result = this.Evaluate("(rand 10)");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(double));

            var value = (double)result;

            Assert.IsTrue(value >= 0);
            Assert.IsTrue(value < 10);
        }

        [TestMethod]
        public void EvaluateRandWithReal()
        {
            var result = this.Evaluate("(rand 2.5)");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(double));

            var value = (double)result;

            Assert.IsTrue(value >= 0);
            Assert.IsTrue(value < 2.5);
        }

        [TestMethod]
        public void EvaluateMethodForms()
        {
            Assert.AreEqual("42", this.Evaluate("(.ToString 42)"));
            Assert.AreEqual("FOO", this.Evaluate("(.ToUpper \"foo\")"));
            Assert.AreEqual("oo", this.Evaluate("(.Substring \"foo\" 1)"));
        }

        [TestMethod]
        public void EvaluateTypeStaticField()
        {
            Assert.AreEqual(System.Math.PI, this.Evaluate("System.Math/PI"));
        }

        [TestMethod]
        public void EvaluateNewString()
        {
            Assert.AreEqual("ccc", this.Evaluate("(new System.String \\c 3)"));
        }

        [TestMethod]
        public void RaiseIfInvalidArityInRand()
        {
            try
            {
                this.Evaluate("(rand 1 2)");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArityException));
                Assert.AreEqual("Wrong number of args (2) passed to ClojSharp.Core.Forms.Rand", ex.Message);
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
