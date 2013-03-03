namespace ClojSharp.Core.Tests.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ListFormTests
    {
        [TestMethod]
        public void ListThreeIntegers()
        {
            ListForm list = new ListForm();
            var result = list.Evaluate(null, new object[] { 1, 2, 3 });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ClojSharp.Core.Language.ISeq));
            Assert.AreEqual("(1 2 3)", result.ToString());
        }

        [TestMethod]
        public void ListOneIntegers()
        {
            ListForm list = new ListForm();
            var result = list.Evaluate(null, new object[] { 1 });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ClojSharp.Core.Language.ISeq));
            Assert.AreEqual("(1)", result.ToString());
        }

        [TestMethod]
        public void ListWithNoArguments()
        {
            ListForm list = new ListForm();
            var result = list.Evaluate(null, new object[] { });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ClojSharp.Core.Language.ISeq));
            Assert.AreSame(EmptyList.Instance, result);
        }

        [TestMethod]
        public void ListWithNullArguments()
        {
            ListForm list = new ListForm();
            var result = list.Evaluate(null, null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ClojSharp.Core.Language.ISeq));
            Assert.AreSame(EmptyList.Instance, result);
        }
    }
}
