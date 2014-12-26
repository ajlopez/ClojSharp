namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;

    public class Macro : BaseMacro
    {
        private IContext closure;
        private IList<string> names;
        private string restname;
        private object body;

        public Macro(IContext closure, IList<string> names, object body)
            : this(closure, names, null, body)
        {
        }

        public Macro(IContext closure, IList<string> names, string restname, object body)
        {
            this.closure = closure;
            this.names = names;
            this.restname = restname;
            this.body = body;
        }

        public override int RequiredArity
        {
            get { if (this.names == null) return 0; return this.names.Count; }
        }

        public override bool VariableArity
        {
            get { return this.restname != null; }
        }

        public override object EvaluateMacro(IContext context, IList<object> arguments)
        {
            return Machine.Evaluate(this.Expand(arguments), context);
        }

        public override object Expand(IList<object> arguments)
        {
            IContext newcontext = new Context(this.closure);

            if (this.names != null && this.names.Count > 0)
                for (int k = 0; k < this.names.Count; k++)
                    newcontext.SetValue(this.names[k], arguments[k]);

            if (this.restname != null)
                newcontext.SetValue(this.restname, this.MakeList(this.names != null ? this.names.Count : 0, arguments));

            return Machine.Evaluate(this.body, newcontext);
        }

        private List MakeList(int nelement, IList<object> elements)
        {
            if (nelement >= elements.Count)
                return null;

            return new List(elements[nelement], this.MakeList(nelement + 1, elements));
        }
    }
}
