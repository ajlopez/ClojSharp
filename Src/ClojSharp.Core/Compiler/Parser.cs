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
        private static Symbol Quote = new Symbol("quote");
        private static string QuoteChar = "'";
        private static string NilName = "nil";
        private static string TrueName = "true";
        private static string FalseName = "false";

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

            if (token.Type == TokenType.Separator && token.Value == "{")
                return this.ParseMap();

            if (token.Type == TokenType.Integer)
                return int.Parse(token.Value);

            if (token.Type == TokenType.Name)
            {
                if (token.Value == QuoteChar)
                    return new List(Quote, new List(this.ParseExpression(), null));
                if (token.Value == NilName)
                    return null;
                if (token.Value == FalseName)
                    return false;
                if (token.Value == TrueName)
                    return true;
                if (token.Value[0] == ':')
                    return new Keyword(token.Value.Substring(1));
            }

            return new Symbol(token.Value);
        }

        private List ParseList()
        {
            Stack<object> elements = new Stack<object>();
            var token = this.lexer.NextToken();

            while (token != null && token.Type != TokenType.Separator || token.Value != ")")
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

        private VectorValue ParseVector()
        {
            List<object> expressions = new List<object>();
            var token = this.lexer.NextToken();

            while (token != null && token.Type != TokenType.Separator || token.Value != "]")
            {
                this.lexer.PushToken(token);
                expressions.Add(this.ParseExpression());
                token = this.lexer.NextToken();
            }

            return new VectorValue(expressions);
        }

        private MapValue ParseMap()
        {
            List<object> expressions = new List<object>();
            var token = this.lexer.NextToken();

            while (token != null && token.Type != TokenType.Separator || token.Value != "}")
            {
                this.lexer.PushToken(token);
                expressions.Add(this.ParseExpression());
                token = this.lexer.NextToken();
            }

            return new MapValue(expressions);
        }
    }
}
