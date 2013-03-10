﻿namespace ClojSharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
        private const string Separators = "()[]";
        private const char StringQuote = '"';
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

            if (Separators.Contains(chr))
                return new Token(TokenType.Separator, chr.ToString());

            if (char.IsDigit(chr))
                return this.NextInteger(chr);

            if (char.IsLetter(chr))
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

            for (ch = this.NextChar(); ch >= 0 && char.IsLetterOrDigit((char)ch); ch = this.NextChar())
                value += (char)ch;

            this.PushChar(ch);

            return new Token(TokenType.Name, value);
        }

        private Token NextString()
        {
            string value = string.Empty;

            int ch;

            for (ch = this.NextChar(); ch >= 0 && (char)ch != StringQuote; ch = this.NextChar())
                value += (char)ch;

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

            this.PushChar(ch);

            return new Token(TokenType.Integer, value);
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

            return this.reader.Read();
        }

        private void PushChar(int ch)
        {
            this.chars.Push(ch);
        }
    }
}
