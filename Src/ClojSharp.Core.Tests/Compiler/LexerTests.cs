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

            IsToken(lexer, TokenType.Name, "name");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GeDotName()
        {
            Lexer lexer = new Lexer(".name");

            IsToken(lexer, TokenType.Name, ".name");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameEndingWithQuestionMark()
        {
            Lexer lexer = new Lexer("name?");

            IsToken(lexer, TokenType.Name, "name?");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameEndingWithInternalMinusSign()
        {
            Lexer lexer = new Lexer("bit-and");

            IsToken(lexer, TokenType.Name, "bit-and");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetQualifiedNameNamespace()
        {
            Lexer lexer = new Lexer("clojsharp.core");

            IsToken(lexer, TokenType.Name, "clojsharp.core");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetQualifiedName()
        {
            Lexer lexer = new Lexer("clojsharp.core/str");

            IsToken(lexer, TokenType.Name, "clojsharp.core/str");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameAndComment()
        {
            Lexer lexer = new Lexer("name ; comment");

            IsToken(lexer, TokenType.Name, "name");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetCommentAndName()
        {
            Lexer lexer = new Lexer("; a comment \r\nname");

            IsToken(lexer, TokenType.Name, "name");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameWithSpaces()
        {
            Lexer lexer = new Lexer("  name   ");

            IsToken(lexer, TokenType.Name, "name");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetQuoteAsName()
        {
            Lexer lexer = new Lexer("'");

            IsToken(lexer, TokenType.Name, "'");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetUnquoteAsName()
        {
            Lexer lexer = new Lexer("~");

            IsToken(lexer, TokenType.Name, "~");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetUnquoteSpliceAsName()
        {
            Lexer lexer = new Lexer("~@");

            IsToken(lexer, TokenType.Name, "~@");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetChar()
        {
            Lexer lexer = new Lexer("\\a");

            IsToken(lexer, TokenType.Character, "a");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetEscapedChar()
        {
            Lexer lexer = new Lexer("\\\\\\ \\\\n \\\\r \\\\t");

            IsToken(lexer, TokenType.Character, "\\");
            IsToken(lexer, TokenType.Character, "\n");
            IsToken(lexer, TokenType.Character, "\r");
            IsToken(lexer, TokenType.Character, "\t");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNumeralQuoteAsName()
        {
            Lexer lexer = new Lexer("#'");

            IsToken(lexer, TokenType.Name, "#'");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNumeralParenthesisAsName()
        {
            Lexer lexer = new Lexer("#(");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("#(", token.Value);

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
        public void GetAmpersandAsName()
        {
            Lexer lexer = new Lexer("&");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("&", token.Value);

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
        public void GetTwoNamesSkippingComma()
        {
            Lexer lexer = new Lexer("name1, name2");

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
        public void GetNegativeInteger()
        {
            Lexer lexer = new Lexer("-123");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.Type);
            Assert.AreEqual("-123", token.Value);

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
        public void GetReal()
        {
            Lexer lexer = new Lexer("123.45");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Real, token.Type);
            Assert.AreEqual("123.45", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNegativeReal()
        {
            Lexer lexer = new Lexer("-123.45");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Real, token.Type);
            Assert.AreEqual("-123.45", token.Value);

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
        public void GetSharpCurlyBracesAsSeparators()
        {
            Lexer lexer = new Lexer("#{}");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual("#{", token.Value);

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
        public void RaiseIfUnclosedString()
        {
            Lexer lexer = new Lexer("\"foo");

            try
            {
                lexer.NextToken();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(LexerException));
                Assert.AreEqual("Unclosed string", ex.Message);
            }
        }

        [TestMethod]
        public void CommaIsWhiteSpace()
        {
            Lexer lexer = new Lexer(", ,, ,,,");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetKeyword()
        {
            Lexer lexer = new Lexer(":a");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Keyword, token.Type);
            Assert.AreEqual("a", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetKeywordEndingWithQuestionMark()
        {
            Lexer lexer = new Lexer(":a?");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Keyword, token.Type);
            Assert.AreEqual("a?", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetAnonymousArgumentWithDigit()
        {
            Lexer lexer = new Lexer("%1");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("%1", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetAnonymousRestArgument()
        {
            Lexer lexer = new Lexer("%&");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("%&", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void RaiseIfAnonymousCharacter()
        {
            Lexer lexer = new Lexer("%");

            try
            {
                lexer.NextToken();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(LexerException));
                Assert.AreEqual("Unexpected '%'", ex.Message);
            }
        }

        [TestMethod]
        public void RaiseIfAnonymousCharacterAndSpace()
        {
            Lexer lexer = new Lexer("% ");

            try
            {
                lexer.NextToken();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(LexerException));
                Assert.AreEqual("Unexpected '%'", ex.Message);
            }
        }

        [TestMethod]
        public void RaiseIfAnonymousCharacterAndLetter()
        {
            Lexer lexer = new Lexer("%a");

            try
            {
                lexer.NextToken();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(LexerException));
                Assert.AreEqual("Unexpected '%a'", ex.Message);
            }
        }

        private static void IsToken(Lexer lexer, TokenType type, string value)
        {
            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(type, token.Type);
            Assert.AreEqual(value, token.Value);
        }
    }
}
