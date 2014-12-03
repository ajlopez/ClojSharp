namespace ClojSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Compiler;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using ClojSharp.Core.SpecialForms;

    public class Machine
    {
        private VarContext root;

        public Machine()
        {
            this.root = new VarContext();

            var ns = new Namespace("clojsharp.core");

            this.root.SetValue("*ns*", ns);

            this.root.SetValue("def", new Def());
            this.root.SetValue("fn", new Fn());
            this.root.SetValue("mfn", new MFn());
            this.root.SetValue("quote", new Quote());
            this.root.SetValue("backquote", new BackQuote());
            this.root.SetValue("let", new Let());
            this.root.SetValue("if", new If());
            this.root.SetValue("do", new Do());
            this.root.SetValue("var", new VarF());
            this.root.SetValue("ns", new Ns());

            this.root.SetValue("cons", new Cons());
            this.root.SetValue("list", new ListForm());
            this.root.SetValue("first", new First());
            this.root.SetValue("rest", new Rest());
            this.root.SetValue("next", new Next());

            this.root.SetValue("+", new Add());
            this.root.SetValue("-", new Subtract());
            this.root.SetValue("*", new Multiply());
            this.root.SetValue("/", new Divide());
            this.root.SetValue("mod", new Mod());
            this.root.SetValue("rem", new Rem());

            this.root.SetValue("and", new And());
            this.root.SetValue("or", new Or());
            this.root.SetValue("not", new Not());
            this.root.SetValue("new", new New());

            this.root.SetValue("nil?", new NilP());
            this.root.SetValue("number?", new NumberP());
            this.root.SetValue("false?", new FalseP());
            this.root.SetValue("true?", new TrueP());
            this.root.SetValue("zero?", new ZeroP());
            this.root.SetValue("integer?", new IntegerP());
            this.root.SetValue("float?", new FloatP());
            this.root.SetValue("char?", new CharP());
            this.root.SetValue("string?", new StringP());
            this.root.SetValue("symbol?", new SymbolP());
            this.root.SetValue("seq?", new SeqP());
            this.root.SetValue("blank?", new BlankP());

            this.root.SetValue("str", new Str());
            this.root.SetValue("rand", new Rand());
            this.root.SetValue("atom", new AtomF());
            this.root.SetValue("deref", new Deref());
            this.root.SetValue("meta", new Meta());
            this.root.SetValue("class", new Class());
            this.root.SetValue("max", new Max());
            this.root.SetValue("min", new Min());
        }

        public IContext RootContext { get { return this.root; } }

        public static IList<object> CompileFile(string filename, IContext context)
        {
            IList<object> expressions = new List<object>();

            TextReader reader = File.OpenText(filename);
            Parser parser = new Parser(reader);

            for (object obj = parser.ParseExpression(); obj != null; obj = parser.ParseExpression())
                expressions.Add(Evaluate(obj, context));

            return expressions;
        }

        public static object EvaluateFile(string filename, IContext context)
        {
            var pgm = new Do();
            return pgm.Evaluate(context, CompileFile(filename, context));
        }

        public static object Evaluate(object obj, IContext context)
        {
            if (obj is IEvaluable)
                return ((IEvaluable)obj).Evaluate(context);

            return obj;
        }

        public object EvaluateFile(string filename)
        {
            return EvaluateFile(filename, this.RootContext);
        }
    }
}
