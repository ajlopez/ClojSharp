namespace ClojSharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class Parser
    {
        private static Symbol quote = new Symbol("quote");
        private static string quoteChar = "'";
        private static Symbol backquote = new Symbol("backquote");
        private static string unquoteChar = "~";
        private static Symbol unquote = new Symbol("unquote");
        private static string backquoteChar = "`";
        private static Symbol withMeta = new Symbol("with-meta");
        private static string metaChar = "^";
        private static Symbol var = new Symbol("var");
        private static Symbol fn = new Symbol("fn");
        private static string derefChar = "@";
        private static Symbol deref = new Symbol("deref");
        private static string varDispatch = "#'";
        private static string anonfnDispatch = "#(";
        private static string nilName = "nil";
        private static string trueName = "true";
        private static string falseName = "false";

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
                return int.Parse(token.Value, CultureInfo.InvariantCulture);

            if (token.Type == TokenType.Real)
                return double.Parse(token.Value, CultureInfo.InvariantCulture);

            if (token.Type == TokenType.Character)
                return token.Value[0];

            if (token.Type == TokenType.String)
                return token.Value;

            if (token.Type == TokenType.Keyword)
                return new Keyword(token.Value);

            if (token.Type == TokenType.Name)
            {
                if (token.Value == quoteChar)
                    return new List(quote, new List(this.ParseExpression(), null));

                if (token.Value == backquoteChar)
                    return new List(backquote, new List(this.ParseExpression(), null));

                if (token.Value == unquoteChar)
                    return new List(unquote, new List(this.ParseExpression(), null));

                if (token.Value == metaChar)
                {
                    var metaexpr = this.ParseExpression();
                    var metadata = (Map)((MapValue)metaexpr).Evaluate(null);
                    var obj = this.ParseExpression();
                    return ((IObject)obj).WithMetadata(metadata);
                }

                if (token.Value == derefChar)
                    return new List(deref, new List(this.ParseExpression(), null));

                if (token.Value == varDispatch)
                    return new List(var, new List(this.ParseExpression(), null));

                if (token.Value == anonfnDispatch)
                    return new List(fn, new List(new Vector(new object[] { }), new List(this.ParseList(), null)));

                if (token.Value == nilName)
                    return null;

                if (token.Value == falseName)
                    return false;

                if (token.Value == trueName)
                    return true;
            }

            return new Symbol(token.Value);
        }

        private List ParseList()
        {
            Stack<object> elements = new Stack<object>();
            var token = this.lexer.NextToken();

            while (token != null && (token.Type != TokenType.Separator || token.Value != ")"))
            {
                this.lexer.PushToken(token);
                elements.Push(this.ParseExpression());
                token = this.lexer.NextToken();
            }

            if (token == null)
                throw new ParserException("Unclosed list");

            List list = null;

            while (elements.Count > 0)
                list = new List(elements.Pop(), list);

            return list;
        }

        private Vector ParseVector()
        {
            List<object> expressions = new List<object>();
            var token = this.lexer.NextToken();

            while (token != null && (token.Type != TokenType.Separator || token.Value != "]"))
            {
                this.lexer.PushToken(token);
                expressions.Add(this.ParseExpression());
                token = this.lexer.NextToken();
            }

            if (token == null)
                throw new ParserException("Unclosed vector");

            return new Vector(expressions);
        }

        private MapValue ParseMap()
        {
            List<object> expressions = new List<object>();
            var token = this.lexer.NextToken();

            while (token != null && (token.Type != TokenType.Separator || token.Value != "}"))
            {
                this.lexer.PushToken(token);
                expressions.Add(this.ParseExpression());
                token = this.lexer.NextToken();
            }

            if (token == null)
                throw new ParserException("Unclosed map");

            return new MapValue(expressions);
        }
    }
}
