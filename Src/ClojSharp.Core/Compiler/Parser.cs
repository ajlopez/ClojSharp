namespace ClojSharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class Parser
    {
        private Lexer lexer;

        public Parser(string text)
        {
            this.lexer = new Lexer(text);
        }

        public object ParseExpression()
        {
            var token = this.lexer.NextToken();

            if (token == null)
                return null;

            if (token.Type == TokenType.Separator && token.Value == "(")
                return this.ParseList();

            if (token.Type == TokenType.Integer)
                return int.Parse(token.Value);

            return new Symbol(token.Value);
        }

        private List ParseList()
        {
            var token = this.lexer.NextToken();

            if (token.Type == TokenType.Separator && token.Value == ")")
                return null;

            this.lexer.PushToken(token);

            return new List(this.ParseExpression(), this.ParseList());
        }
    }
}
