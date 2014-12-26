namespace ClojSharp.Core.Tests.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.SpecialForms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MultiMacroTests
    {
        [TestMethod]
        public void EvaluateMultiMacro()
        {
            Macro macro1 = new Macro(null, null, 0);
            Macro macro2 = new Macro(null, new string[] { "x" }, 1);
            Macro macro3 = new Macro(null, new string[] { "x", "y" }, 2);

            MultiMacro macro = new MultiMacro(new Macro[] { macro1, macro2, macro3 });

            Assert.IsTrue(macro.VariableArity);

            Assert.AreEqual(0, macro.Evaluate(null, null));
            Assert.AreEqual(0, macro.Evaluate(null, new object[] { }));
            Assert.AreEqual(1, macro.Evaluate(null, new object[] { 1 }));
            Assert.AreEqual(2, macro.Evaluate(null, new object[] { 1, 2 }));
        }

        [TestMethod]
        public void EvaluateMultiMacroWithVariableArguments()
        {
            Macro macro1 = new Macro(null, null, 0);
            Macro macro2 = new Macro(null, new string[] { "x" }, 1);
            Macro macro3 = new Macro(null, new string[] { "x", "y" }, 2);
            Macro macro4 = new Macro(null, new string[] { "x", "y" }, "rest", 3);

            MultiMacro macro = new MultiMacro(new Macro[] { macro1, macro2, macro3, macro4 });

            Assert.AreEqual(3, macro.Evaluate(null, new object[] { 1, 2, 3, 4 }));
        }

        [TestMethod]
        public void VariableArity()
        {
            Macro macro1 = new Macro(null, new string[] { "x", "y" }, "rest", 3);

            MultiMacro macro = new MultiMacro(new Macro[] { macro1 });

            Assert.IsTrue(macro.VariableArity);
        }

        [TestMethod]
        public void ExpandMultiMacroWithVariableArguments()
        {
            Macro macro1 = new Macro(null, null, 0);
            Macro macro2 = new Macro(null, new string[] { "x" }, 1);
            Macro macro3 = new Macro(null, new string[] { "x", "y" }, 2);
            Macro macro4 = new Macro(null, new string[] { "x", "y" }, "rest", 3);

            MultiMacro macro = new MultiMacro(new Macro[] { macro1, macro2, macro3, macro4 });

            Assert.AreEqual(3, macro.Expand(new object[] { 1, 2, 3, 4 }));
        }

        [TestMethod]
        public void ExpandMultiMacroWithArityTwo()
        {
            Macro macro1 = new Macro(null, null, 0);
            Macro macro2 = new Macro(null, new string[] { "x" }, 1);
            Macro macro3 = new Macro(null, new string[] { "x", "y" }, 2);
            Macro macro4 = new Macro(null, new string[] { "x", "y" }, "rest", 3);

            MultiMacro macro = new MultiMacro(new Macro[] { macro1, macro2, macro3, macro4 });

            Assert.AreEqual(2, macro.Expand(new object[] { 1, 2 }));
        }

        [TestMethod]
        public void ExpandMultiMacroWithArityZero()
        {
            Macro macro1 = new Macro(null, null, 0);
            Macro macro2 = new Macro(null, new string[] { "x" }, 1);
            Macro macro3 = new Macro(null, new string[] { "x", "y" }, 2);
            Macro macro4 = new Macro(null, new string[] { "x", "y" }, "rest", 3);

            MultiMacro macro = new MultiMacro(new Macro[] { macro1, macro2, macro3, macro4 });

            Assert.AreEqual(0, macro.Expand(null));
        }

        [TestMethod]
        public void RaiseIfInvalidArityOnEvaluate()
        {
            Macro macro1 = new Macro(null, null, 0);
            Macro macro3 = new Macro(null, new string[] { "x", "y" }, 2);
            Macro macro4 = new Macro(null, new string[] { "x", "y" }, "rest", 3);

            MultiMacro macro = new MultiMacro(new Macro[] { macro1, macro3, macro4 });

            try
            {
                macro.Evaluate(null, new object[] { 1 });
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArityException));
                Assert.AreEqual("Wrong number of args (1) passed to ClojSharp.Core.SpecialForms.MultiMacro", ex.Message);
            }
        }

        [TestMethod]
        public void RaiseIfInvalidArityOnExpand()
        {
            Macro macro1 = new Macro(null, null, 0);
            Macro macro3 = new Macro(null, new string[] { "x", "y" }, 2);
            Macro macro4 = new Macro(null, new string[] { "x", "y" }, "rest", 3);

            MultiMacro macro = new MultiMacro(new Macro[] { macro1, macro3, macro4 });

            try
            {
                macro.Expand(new object[] { 1 });
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArityException));
                Assert.AreEqual("Wrong number of args (1) passed to ClojSharp.Core.SpecialForms.MultiMacro", ex.Message);
            }
        }
    }
}
