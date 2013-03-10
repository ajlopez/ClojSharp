namespace ClojSharp.Core.Tests.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Compiler;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void GetNull()
        {
            Lexer lexer = new Lexer((string)null);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetName()
        {
            Lexer lexer = new Lexer("name");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("name", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameWithSpaces()
        {
            Lexer lexer = new Lexer("  name   ");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("name", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetPlusAsName()
        {
            Lexer lexer = new Lexer("+");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("+", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetTwoSymbolsAsName()
        {
            Lexer lexer = new Lexer("+=");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("+=", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetTwoNames()
        {
            Lexer lexer = new Lexer("name1 name2");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("name1", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("name2", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameInParentheses()
        {
            Lexer lexer = new Lexer("(name)");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual("(", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("name", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual(")", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetInteger()
        {
            Lexer lexer = new Lexer("123");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.Type);
            Assert.AreEqual("123", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetIntegerInParentheses()
        {
            Lexer lexer = new Lexer("(123)");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual("(", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.Type);
            Assert.AreEqual("123", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual(")", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetLeftParenthesis()
        {
            Lexer lexer = new Lexer("(");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual("(", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetParentheses()
        {
            Lexer lexer = new Lexer("()");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual("(", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual(")", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetBracketsAsSeparators()
        {
            Lexer lexer = new Lexer("[]");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual("[", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual("]", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetCurlyBracesAsSeparators()
        {
            Lexer lexer = new Lexer("{}");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual("{", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual("}", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetString()
        {
            Lexer lexer = new Lexer("\"foo\"");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.String, token.Type);
            Assert.AreEqual("foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void CommaIsWhiteSpace()
        {
            Lexer lexer = new Lexer(", ,, ,,,");

            Assert.IsNull(lexer.NextToken());
        }
    }
}
