﻿namespace ClojSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using ClojSharp.Core.SpecialForms;
    using System.IO;
    using ClojSharp.Core.Compiler;

    public class Machine
    {
        private IContext root;

        public Machine()
        {
            this.root = new VarContext();

            this.root.SetValue("def", new Def());
            this.root.SetValue("fn", new Fn());
            this.root.SetValue("quote", new Quote());
            this.root.SetValue("let", new Let());
            this.root.SetValue("if", new If());
            this.root.SetValue("do", new Do());
            this.root.SetValue("var", new VarF());

            this.root.SetValue("cons", new Cons());
            this.root.SetValue("list", new ListForm());
            this.root.SetValue("first", new First());
            this.root.SetValue("rest", new Rest());
            this.root.SetValue("next", new Next());
            this.root.SetValue("+", new Add());
            this.root.SetValue("-", new Subtract());
            this.root.SetValue("*", new Multiply());
            this.root.SetValue("/", new Divide());
        }

        public IContext RootContext { get { return this.root; } }

        public object EvaluateFile(string filename)
        {
            return EvaluateFile(filename, this.RootContext);
        }

        public static object EvaluateFile(string filename, IContext context)
        {
            TextReader reader = File.OpenText(filename);
            Parser parser = new Parser(reader);
            object result = null;

            for (object obj = parser.ParseExpression(); obj != null; obj = parser.ParseExpression())
                result = Evaluate(obj, context);

            return result;
        }

        public static object Evaluate(object obj, IContext context)
        {
            if (obj is IEvaluable)
                return ((IEvaluable)obj).Evaluate(context);

            return obj;
        }

        public static bool IsFalse(object obj)
        {
            return obj == null || obj.Equals(false);
        }
    }
}
