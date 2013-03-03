namespace ClojSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using ClojSharp.Core.SpecialForms;

    public class Machine
    {
        private Context root;

        public Machine()
        {
            this.root = new Context();
            this.root.SetValue("def", new Def());
            this.root.SetValue("fn", new Fn());
            this.root.SetValue("quote", new Quote());
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

        public Context RootContext { get { return this.root; } }

        public object Evaluate(object obj, Context context)
        {
            if (obj is IEvaluable)
                return ((IEvaluable)obj).Evaluate(context);

            return obj;
        }
    }
}
