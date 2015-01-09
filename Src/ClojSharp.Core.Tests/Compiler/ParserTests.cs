namespace ClojSharp.Core.Tests.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
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
            Parser parser = new Parser((string)null);

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
        public void ParseIntegerUsingAReader()
        {
            Parser parser = new Parser(new StringReader("123"));

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(int));

            var value = (int)expr;

            Assert.AreEqual(123, value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseReal()
        {
            Parser parser = new Parser("123.45");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(double));

            var value = (double)expr;

            Assert.AreEqual(123.45, value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseString()
        {
            Parser parser = new Parser("\"foo\"");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(string));

            var value = (string)expr;

            Assert.AreEqual("foo", value);

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

            Assert.IsNull(list.Next);
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

            Assert.IsNotNull(list.Next);
            Assert.IsInstanceOfType(list.Next, typeof(List));

            list = (List)list.Rest;

            Assert.IsNotNull(list.First);
            Assert.IsInstanceOfType(list.First, typeof(Symbol));
            Assert.AreEqual("name2", ((Symbol)list.First).Name);
            Assert.IsNull(list.Next);
            Assert.AreSame(EmptyList.Instance, list.Rest);
        }

        [TestMethod]
        public void ParseAndEvaluateVectorWithTwoIntegers()
        {
            Parser parser = new Parser("[1 2]");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(Vector));

            var vectorvalue = (Vector)expr;

            Assert.IsNull(vectorvalue.Metadata);

            var value = vectorvalue.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(Vector));

            var vector = (Vector)value;

            Assert.IsNull(vector.Metadata);

            Assert.IsNotNull(vector.Elements);
            Assert.AreEqual(2, vector.Elements.Count);
            Assert.AreEqual(1, vector.Elements[0]);
            Assert.AreEqual(2, vector.Elements[1]);
        }

        [TestMethod]
        public void ParseAndEvaluateVectorWithTwoIntegersAndMetadata()
        {
            Parser parser = new Parser("^{:foo true} [1 2]");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(Vector));

            var vectorvalue = (Vector)expr;

            Assert.IsNotNull(vectorvalue.Metadata);
            Assert.AreEqual(true, vectorvalue.Metadata.GetValue(new Keyword("foo")));

            var value = vectorvalue.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(Vector));

            var vector = (Vector)value;

            Assert.IsNotNull(vector.Metadata);
            Assert.AreEqual(true, vector.Metadata.GetValue(new Keyword("foo")));

            Assert.IsNotNull(vector.Elements);
            Assert.AreEqual(2, vector.Elements.Count);
            Assert.AreEqual(1, vector.Elements[0]);
            Assert.AreEqual(2, vector.Elements[1]);
        }

        [TestMethod]
        public void ParseQuotedList()
        {
            Parser parser = new Parser("'(1 2)");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(List));
            Assert.AreEqual("(quote (1 2))", expr.ToString());
        }

        [TestMethod]
        public void ParseBackQuotedList()
        {
            Parser parser = new Parser("`(1 2)");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(List));
            Assert.AreEqual("(backquote (1 2))", expr.ToString());
        }

        [TestMethod]
        public void UnquotedSymbol()
        {
            Parser parser = new Parser("~x");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(List));
            Assert.AreEqual("(unquote x)", expr.ToString());
        }

        [TestMethod]
        public void UnquoteSplicingSymbol()
        {
            Parser parser = new Parser("~@x");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(List));
            Assert.AreEqual("(unquote-splice x)", expr.ToString());
        }

        [TestMethod]
        public void ParseMetadata()
        {
            Parser parser = new Parser("^{:a 1 :b 2} [1 2 3]");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(Vector));
            Assert.AreEqual("[1 2 3]", expr.ToString());

            var value = (Vector)expr;

            Assert.IsNotNull(value.Metadata);
            Assert.AreEqual(1, value.Metadata.GetValue(new Keyword("a")));
            Assert.AreEqual(2, value.Metadata.GetValue(new Keyword("b")));
        }

        [TestMethod]
        public void ParseDeref()
        {
            Parser parser = new Parser("@foo");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(List));
            Assert.AreEqual("(deref foo)", expr.ToString());
        }

        [TestMethod]
        public void ParseVarMacro()
        {
            Parser parser = new Parser("#'x");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(List));
            Assert.AreEqual("(var x)", expr.ToString());
        }

        [TestMethod]
        public void ParseAnonymousFunctionMacroWithoutArguments()
        {
            Parser parser = new Parser("#(+ 1 2)");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(List));
            Assert.AreEqual("(fn [] (+ 1 2))", expr.ToString());
        }

        [TestMethod]
        public void ParseNilAsNull()
        {
            Parser parser = new Parser("nil");

            var expr = parser.ParseExpression();

            Assert.IsNull(expr);
        }

        [TestMethod]
        public void ParseCharacter()
        {
            Parser parser = new Parser("\\a");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsTrue(expr is char);
        }

        [TestMethod]
        public void ParseFalseAsFalse()
        {
            Parser parser = new Parser("false");

            var expr = parser.ParseExpression();

            Assert.AreEqual(false, expr);
        }

        [TestMethod]
        public void ParseTrueAsTrue()
        {
            Parser parser = new Parser("true");

            var expr = parser.ParseExpression();

            Assert.AreEqual(true, expr);
        }

        [TestMethod]
        public void ParseKeyword()
        {
            Parser parser = new Parser(":a");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(Keyword));

            var keyword = (Keyword)expr;

            Assert.AreEqual("a", keyword.Name);
            Assert.AreEqual(":a", keyword.ToString());
        }

        [TestMethod]
        public void ParseAndEvaluateMap()
        {
            Parser parser = new Parser("{:a 1 :b 2}");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(Map));

            var mapvalue = (Map)expr;

            Assert.IsNull(mapvalue.Metadata);
            Assert.AreEqual("{:a 1 :b 2}", mapvalue.ToString()); 

            var value = mapvalue.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(Map));

            var map = (Map)value;

            Assert.AreEqual(1, map.GetValue(new Keyword("a")));
            Assert.AreEqual(2, map.GetValue(new Keyword("b")));
            Assert.IsNull(map.GetValue(new Keyword("c")));
        }

        [TestMethod]
        public void ParseAndEvaluateSet()
        {
            Parser parser = new Parser("#{:a :b :d}");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(Set));

            var value = Machine.Evaluate(expr, null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(Set));

            var set = (Set)value;

            Assert.IsNull(set.Metadata);
            Assert.IsTrue(set.HasKey(new Keyword("a")));
            Assert.IsTrue(set.HasKey(new Keyword("b")));
            Assert.IsFalse(set.HasKey(new Keyword("c")));
            Assert.IsTrue(set.HasKey(new Keyword("d")));
        }

        [TestMethod]
        public void ParseAndEvaluateSetWithExpressions()
        {
            Parser parser = new Parser("#{(+ 1 0) (+ 1 1) (+ 1 2)}");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(Set));

            Machine machine = new Machine();
            var value = Machine.Evaluate(expr, machine.RootContext);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(Set));

            var set = (Set)value;

            Assert.IsNull(set.Metadata);
            Assert.IsTrue(set.HasKey(1));
            Assert.IsTrue(set.HasKey(2));
            Assert.IsTrue(set.HasKey(3));
            Assert.IsFalse(set.HasKey(4));
        }

        [TestMethod]
        public void ParseAndEvaluateMapWithMetadata()
        {
            Parser parser = new Parser("^{:foo true} {:a 1 :b 2}");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(Map));

            var mapvalue = (Map)expr;

            Assert.IsNotNull(mapvalue.Metadata);
            Assert.AreEqual(true, mapvalue.Metadata.GetValue(new Keyword("foo")));

            var value = mapvalue.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(Map));

            var map = (Map)value;

            Assert.IsNotNull(map.Metadata);
            Assert.AreEqual(true, map.Metadata.GetValue(new Keyword("foo")));

            Assert.AreEqual(1, map.GetValue(new Keyword("a")));
            Assert.AreEqual(2, map.GetValue(new Keyword("b")));
            Assert.IsNull(map.GetValue(new Keyword("c")));
        }

        [TestMethod]
        public void RaiseWhenUnclosedList()
        {
            Parser parser = new Parser("(1 2 3");

            try
            {
                parser.ParseExpression();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual(ex.Message, "Unclosed list");
            }
        }

        [TestMethod]
        public void RaiseWhenUnclosedVector()
        {
            Parser parser = new Parser("[1 2 3");

            try
            {
                parser.ParseExpression();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual(ex.Message, "Unclosed vector");
            }
        }

        [TestMethod]
        public void RaiseWhenUnclosedMap()
        {
            Parser parser = new Parser("{1 2 3");

            try
            {
                parser.ParseExpression();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual(ex.Message, "Unclosed map");
            }
        }

        [TestMethod]
        public void RaiseWhenUnclosedSet()
        {
            Parser parser = new Parser("#{1 2 3");

            try
            {
                parser.ParseExpression();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual(ex.Message, "Unclosed set");
            }
        }
    }
}
