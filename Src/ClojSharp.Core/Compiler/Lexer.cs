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

            for (int ch = this.NextChar(); ch >= 0; ch = this.NextChar())
                value += (char)ch;

            if (string.IsNullOrEmpty(value))
                return null;

            return new Token(TokenType.Name, value);
        }

        private int NextChar()
        {
            return this.reader.Read();
        }
    }
}
