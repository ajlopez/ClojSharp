namespace ClojSharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using ClojSharp.Core.SpecialForms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EmptyListTests
    {
        [TestMethod]
        public void Length()
        {
            Assert.AreEqual(0, EmptyList.Instance.Length);
        }

        [TestMethod]
        public void EmptyListToString()
        {
            Assert.AreEqual("()", EmptyList.Instance.ToString());
        }
    }
}
