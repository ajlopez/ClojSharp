namespace ClojSharp.Core.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClojSharp.Core.Language;

    [TestClass]
    public class KeywordTests
    {
        [TestMethod]
        public void CreateKeyword()
        {
            Keyword keyword = new Keyword("a");

            Assert.AreEqual("a", keyword.Name);
            Assert.AreEqual(":a", keyword.ToString());
        }

        [TestMethod]
        public void Equals()
        {
            Keyword keyword1 = new Keyword("a");
            Keyword keyword2 = new Keyword("b");
            Keyword keyword3 = new Keyword("a");

            Assert.IsTrue(keyword1.Equals(keyword3));
            Assert.IsTrue(keyword3.Equals(keyword1));
            Assert.AreEqual(keyword1.GetHashCode(), keyword3.GetHashCode());

            Assert.IsFalse(keyword1.Equals(null));
            Assert.IsFalse(keyword1.Equals(123));
            Assert.IsFalse(keyword1.Equals(keyword2));
            Assert.IsFalse(keyword2.Equals(keyword1));
        }
    }
}
