namespace ClojSharp.Core.Tests.Forms
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClojSharp.Core.Forms;

    [TestClass]
    public class FunctionTests
    {
        [TestMethod]
        public void FunctionEvaluateString()
        {
            Function function = new Function(null, null, "foo");

            Assert.AreEqual("foo", function.Evaluate(null, null));
        }
    }
}
