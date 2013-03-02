﻿namespace ClojSharp.Core.Tests.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Compiler;
    using ClojSharp.Core.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseNull()
        {
            Parser parser = new Parser(null);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSymbol()
        {
            Parser parser = new Parser("name");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(Symbol));

            var symbol = (Symbol)expr;

            Assert.AreEqual("name", symbol.Name);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseInteger()
        {
            Parser parser = new Parser("123");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(int));

            var value = (int)expr;

            Assert.AreEqual(123, value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseListWithOneSymbol()
        {
            Parser parser = new Parser("(name)");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(List));

            var list = (List)expr;

            Assert.IsNotNull(list.First);
            Assert.IsInstanceOfType(list.First, typeof(Symbol));
            Assert.AreEqual("name", ((Symbol)list.First).Name);

            Assert.IsNull(list.Rest);
        }

        [TestMethod]
        public void ParseListWithTwoSymbols()
        {
            Parser parser = new Parser("(name1 name2)");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(List));

            var list = (List)expr;

            Assert.IsNotNull(list.First);
            Assert.IsInstanceOfType(list.First, typeof(Symbol));
            Assert.AreEqual("name1", ((Symbol)list.First).Name);

            Assert.IsNotNull(list.Rest);
            Assert.IsInstanceOfType(list.Rest, typeof(List));

            list = (List)list.Rest;

            Assert.IsNotNull(list.First);
            Assert.IsInstanceOfType(list.First, typeof(Symbol));
            Assert.AreEqual("name2", ((Symbol)list.First).Name);
            Assert.IsNull(list.Rest);
        }

        [TestMethod]
        public void ParseVectorWithTwoIntegers()
        {
            Parser parser = new Parser("[1 2]");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(Vector));

            var vector = (Vector)expr;

            Assert.IsNotNull(vector.Elements);
            Assert.AreEqual(2, vector.Elements.Count);
            Assert.AreEqual(1, vector.Elements[0]);
            Assert.AreEqual(2, vector.Elements[1]);
        }
    }
}
