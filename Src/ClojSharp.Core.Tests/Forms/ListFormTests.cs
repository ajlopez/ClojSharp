namespace ClojSharp.Core.Tests.Forms
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClojSharp.Core.Forms;

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
    }
}
