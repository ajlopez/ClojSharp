namespace ClojSharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
        private const string Separators = "()[]{}";
        private const char StringQuote = '"';
        private const char CharacterChar = '\\';
        private const char KeywordChar = ':';
        private const char CommentChar = ';';
        private const char AnonymousChar = '%';
        private const char DispatchChar = '#';

        private TextReader reader;
        private Stack<int> chars = new Stack<int>();
        private Stack<Token> tokens = new Stack<Token>();

        public Lexer(string text)
            : this(new StringReader(text ?? string.Empty))
        {
        }

        public Lexer(TextReader reader)
        {
            this.reader = reader;
        }

        public Token NextToken()
        {
            if (this.tokens.Count > 0)
                return this.tokens.Pop();

            string value = string.Empty;

            int ch = this.NextCharSkippingSpaces();

            if (ch < 0)
                return null;

            char chr = (char)ch;

            if (chr == StringQuote)
                return this.NextString();

            if (chr == KeywordChar)
                return this.NextKeyword();

            if (chr == CharacterChar)
            {
                chr = (char)this.NextChar();
                return new Token(TokenType.Character, chr.ToString());
            }

            if (chr == DispatchChar)
            {
                int ch2 = this.NextChar();

                if (ch2 < 0)
                    throw new LexerException("Unexpected '#'");

                char chr2 = (char)ch2;

                return new Token(TokenType.Name, chr.ToString() + chr2.ToString());
            }

            if (chr == AnonymousChar)
            {
                int ch2 = this.NextChar();

                if (ch2 < 0)
                    throw new LexerException("Unexpected '%'");

                char chr2 = (char)ch2;

                if (!char.IsDigit(chr2) && chr2 != '&')
                    if (char.IsWhiteSpace(chr2))
                        throw new LexerException("Unexpected '%'");
                    else
                        throw new LexerException(string.Format("Unexpected '{0}'", chr.ToString() + (chr2).ToString()));

                return new Token(TokenType.Name, chr.ToString() + (chr2).ToString());
            }

            if (Separators.Contains(chr))
                return new Token(TokenType.Separator, chr.ToString());

            if (chr == '-')
            {
                int ch2 = this.NextChar();
                this.PushChar(ch2);

                if (ch2 >= 0 && char.IsDigit((char)ch2))
                    return this.NextInteger(chr);
            }

            if (char.IsDigit(chr))
                return this.NextInteger(chr);

            if (chr == '.' || char.IsLetter(chr))
                return this.NextName(chr);

            return this.NextSymbolName(chr);
        }

        public void PushToken(Token token)
        {
            this.tokens.Push(token);
        }

        private Token NextName(char chr)
        {
            string value = chr.ToString();
            int ch;

            for (ch = this.NextChar(); ch >= 0 && (ch == '.' || ch == '-' || ch == '/' || char.IsLetterOrDigit((char)ch)); ch = this.NextChar())
                value += (char)ch;

            if (ch >= 0 && (char)ch == '?')
                value += (char)ch;
            else
                this.PushChar(ch);

            return new Token(TokenType.Name, value);
        }

        private Token NextKeyword()
        {
            string value = string.Empty;
            int ch;

            for (ch = this.NextChar(); ch >= 0 && (ch == '.' || ch == '-' || char.IsLetterOrDigit((char)ch)); ch = this.NextChar())
                value += (char)ch;

            if (ch >= 0 && (char)ch == '?')
                value += (char)ch;
            else
                this.PushChar(ch);

            return new Token(TokenType.Keyword, value);
        }

        private Token NextString()
        {
            string value = string.Empty;

            int ch;

            for (ch = this.NextChar(); ch >= 0 && (char)ch != StringQuote; ch = this.NextChar())
                value += (char)ch;

            if (ch < 0)
                throw new LexerException("Unclosed string");

            return new Token(TokenType.String, value);
        }

        private Token NextSymbolName(char chr)
        {
            string value = chr.ToString();
            int ch;

            for (ch = this.NextChar(); ch >= 0 && !char.IsWhiteSpace((char)ch) && !char.IsLetterOrDigit((char)ch) && !Separators.Contains((char)ch); ch = this.NextChar())
                value += (char)ch;

            this.PushChar(ch);

            return new Token(TokenType.Name, value);
        }

        private Token NextInteger(char chr)
        {
            string value = chr.ToString();
            int ch;

            for (ch = this.NextChar(); ch >= 0 && char.IsDigit((char)ch); ch = this.NextChar())
                value += (char)ch;

            if (ch >= 0 && (char)ch == '.')
                return this.NextReal(value);

            this.PushChar(ch);

            return new Token(TokenType.Integer, value);
        }

        private Token NextReal(string intvalue)
        {
            string value = intvalue + ".";
            int ch;

            for (ch = this.NextChar(); ch >= 0 && char.IsDigit((char)ch); ch = this.NextChar())
                value += (char)ch;

            this.PushChar(ch);

            return new Token(TokenType.Real, value);
        }

        private int NextCharSkippingSpaces()
        {
            int ch;

            for (ch = this.NextChar(); ch >= 0 && ((char)ch == ',' || char.IsWhiteSpace((char)ch)); ch = this.NextChar()) 
            {
            }

            return ch;
        }

        private int NextChar()
        {
            if (this.chars.Count > 0)
                return this.chars.Pop();

            int ich = this.reader.Read();

            if (ich >= 0 && (char)ich == CommentChar)
                for (ich = this.reader.Read(); ich >= 0 && (char)ich != '\n';)
                    ich = this.reader.Read();

            return ich;
        }

        private void PushChar(int ch)
        {
            this.chars.Push(ch);
        }
    }
}
