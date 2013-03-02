namespace ClojSharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
        private const string separators = "()[]";
        private TextReader reader;
        private Stack<int> chars = new Stack<int>();
        private Stack<Token> tokens = new Stack<Token>();

        public Lexer(string text)
            : this(new StringReader(text ?? string.Empty))
        {
        }

        public Lexer(StringReader reader)
        {
            this.reader = reader;
        }

        public Token NextToken()
        {
            if (tokens.Count > 0)
                return tokens.Pop();

            string value = string.Empty;

            int ch = this.NextCharSkippingSpaces();

            if (ch < 0)
                return null;

            char chr = (char)ch;

            if (separators.Contains(chr))
                return new Token(TokenType.Separator, chr.ToString());

            if (char.IsDigit(chr))
                return this.NextInteger(chr);

            return this.NextName(chr);
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

            for (ch = this.NextChar(); ch >= 0 && char.IsWhiteSpace((char)ch); ch = this.NextChar())
                ;

            return ch;
        }

        private int NextChar()
        {
            if (chars.Count > 0)
                return chars.Pop();

            return this.reader.Read();
        }

        private void PushChar(int ch)
        {
            this.chars.Push(ch);
        }
    }
}
