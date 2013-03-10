﻿namespace ClojSharp.Core.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClojSharp.Core.Language;

    [TestClass]
    public class SymbolTests
    {
        [TestMethod]
        public void CreateSymbol()
        {
            Symbol symbol = new Symbol("a");

            Assert.AreEqual("a", symbol.Name);
            Assert.AreEqual("a", symbol.ToString());
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
