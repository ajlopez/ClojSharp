namespace ClojSharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
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

        public Parser(TextReader reader)
        {
            this.lexer = new Lexer(reader);
        }

        public object ParseExpression()
        {
            var token = this.lexer.NextToken();

            if (token == null)
                return null;

            if (token.Type == TokenType.Separator && token.Value == "(")
                return this.ParseList();

            if (token.Type == TokenType.Separator && token.Value == "[")
                return this.ParseVector();

            if (token.Type == TokenType.Integer)
                return int.Parse(token.Value);

            if (token.Type == TokenType.Name && token.Value == "'")
                return new List(new Symbol("quote"), new List(this.ParseExpression(), null));

            return new Symbol(token.Value);
        }

        private List ParseList()
        {
            Stack<object> elements = new Stack<object>();
            var token = this.lexer.NextToken();

            while (token.Type != TokenType.Separator || token.Value != ")")
            {
                this.lexer.PushToken(token);
                elements.Push(this.ParseExpression());
                token = this.lexer.NextToken();
            }

            List list = null;

            while (elements.Count > 0)
                list = new List(elements.Pop(), list);

            return list;
        }

        private Vector ParseVector()
        {
            List<object> elements = new List<object>();
            var token = this.lexer.NextToken();

            while (token.Type != TokenType.Separator || token.Value != "]")
            {
                this.lexer.PushToken(token);
                elements.Add(this.ParseExpression());
                token = this.lexer.NextToken();
            }

            return new Vector(elements);
        }
    }
}
