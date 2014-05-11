namespace ClojSharp.Core.Tests.Forms
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Exceptions;

    [TestClass]
    public class MultiFunctionTests
    {
        [TestMethod]
        public void EvaluateMultiFunction()
        {
            Function function1 = new Function(null, null, 0);
            Function function2 = new Function(null, new string[] { "x" }, 1);
            Function function3 = new Function(null, new string[] { "x", "y" }, 2);

            MultiFunction function = new MultiFunction(new Function[] { function1, function2, function3 });

            Assert.AreEqual(0, function.Evaluate(null, null));
            Assert.AreEqual(0, function.Evaluate(null, new object[] { }));
            Assert.AreEqual(1, function.Evaluate(null, new object[] { 1 }));
            Assert.AreEqual(2, function.Evaluate(null, new object[] { 1, 2 }));
        }

        [TestMethod]
        public void EvaluateMultiFunctionWithVariableArguments()
        {
            Function function1 = new Function(null, null, 0);
            Function function2 = new Function(null, new string[] { "x" }, 1);
            Function function3 = new Function(null, new string[] { "x", "y" }, 2);
            Function function4 = new Function(null, new string[] { "x", "y" }, "rest", 3);

            MultiFunction function = new MultiFunction(new Function[] { function1, function2, function3, function4 });

            Assert.AreEqual(3, function.Evaluate(null, new object[] { 1, 2, 3, 4 }));
        }

        [TestMethod]
        public void RaiseIfInvalidArity()
        {
            Function function1 = new Function(null, null, 0);
            Function function3 = new Function(null, new string[] { "x", "y" }, 2);
            Function function4 = new Function(null, new string[] { "x", "y" }, "rest", 3);

            MultiFunction function = new MultiFunction(new Function[] { function1, function3, function4 });

            try
            {
                function.Evaluate(null, new object[] { 1 });
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArityException));
                Assert.AreEqual("Wrong number of args (1) passed to ClojSharp.Core.Forms.MultiFunction", ex.Message);
            }
        }
    }
}
