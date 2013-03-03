namespace ClojSharp.Core.Tests.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;
    using ClojSharp.Core.SpecialForms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class QuoteTests
    {
        [TestMethod]
        public void QuoteInteger()
        {
            Quote quote = new Quote();
            Assert.AreEqual(1, quote.Evaluate(null, new object[] { 1 }));
        }

        [TestMethod]
        public void QuoteIntegers()
        {
            Quote quote = new Quote();
            Assert.AreEqual(1, quote.Evaluate(null, new object[] { 1, 2, 3 }));
        }

        [TestMethod]
        public void QuoteList()
        {
            Quote quote = new Quote();

            var result = quote.Evaluate(null, new object[] { new List(1, new List(2, null)) });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List));

            var list = (List)result;
            Assert.AreEqual(1, list.First);
            Assert.IsInstanceOfType(list.Rest, typeof(List));
            Assert.AreEqual(2, ((List)list.Rest).First);
            Assert.IsNull(((List)list.Rest).Rest);
        }
    }
}
