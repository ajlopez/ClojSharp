namespace ClojSharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
        private TextReader reader;

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
            string value = string.Empty;

            int ch = this.NextChar();

            if (ch < 0)
                return null;

            char chr = (char)ch;

            if (char.IsDigit(chr))
                return this.NextInteger(chr);

            return this.NextName(chr);
        }

        private Token NextName(char chr)
        {
            string value = chr.ToString();

            for (int ch = this.NextChar(); ch >= 0; ch = this.NextChar())
                value += (char)ch;

            return new Token(TokenType.Name, value);
        }

        private Token NextInteger(char chr)
        {
            string value = chr.ToString();

            for (int ch = this.NextChar(); ch >= 0 && char.IsDigit((char)ch); ch = this.NextChar())
                value += (char)ch;

            return new Token(TokenType.Integer, value);
        }

        private int NextChar()
        {
            return this.reader.Read();
        }
    }
}
