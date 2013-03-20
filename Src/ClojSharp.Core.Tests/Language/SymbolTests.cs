namespace ClojSharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SymbolTests
    {
        [TestMethod]
        public void CreateSymbol()
        {
            Symbol symbol = new Symbol("a");

            Assert.AreEqual("a", symbol.Name);
            Assert.AreEqual("a", symbol.ToString());
            Assert.IsNull(symbol.Metadata);
        }

        [TestMethod]
        public void SetGetMetadata()
        {
            Symbol symbol = new Symbol("a");
            Map map = new Map(new object[] { });

            symbol.Metadata = map;

            Assert.AreSame(map, symbol.Metadata);
        }

        [TestMethod]
        public void ExceptionWhenResetMetadata()
        {
            Symbol symbol = new Symbol("a");
            Map map = new Map(new object[] { });
            Map newmap = new Map(new object[] { });

            symbol.Metadata = map;

            try
            {
                symbol.Metadata = newmap;
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(RuntimeException));
                Assert.AreEqual("metadata already set", ex.Message);
            }
        }

        [TestMethod]
        public void Equals()
        {
            Symbol symbol1 = new Symbol("a");
            Symbol symbol2 = new Symbol("b");
            Symbol symbol3 = new Symbol("a");

            Assert.IsTrue(symbol1.Equals(symbol3));
            Assert.IsTrue(symbol3.Equals(symbol1));
            Assert.AreEqual(symbol1.GetHashCode(), symbol3.GetHashCode());

            Assert.IsFalse(symbol1.Equals(null));
            Assert.IsFalse(symbol1.Equals(123));
            Assert.IsFalse(symbol1.Equals(symbol2));
            Assert.IsFalse(symbol2.Equals(symbol1));
        }
    }
}
