namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;

    public class Function : BaseForm
    {
        private IContext closure;
        private IList<string> names;
        private string restname;
        private object body;
        private IEvaluable evalbody;
        private int arity;

        public Function(IContext closure, IList<string> names, object body)
            : this(closure, names, null, body)
        {
        }

        public Function(IContext closure, IList<string> names, string restname, object body)
        {
            this.closure = closure;
            this.names = names;
            this.restname = restname;
            this.body = body;
            this.arity = names == null ? 0 : names.Count;

            if (body is IEvaluable)
                this.evalbody = (IEvaluable)body;
        }

        public override int RequiredArity
        {
            get { return this.arity; }
        }

        public override bool VariableArity { get { return this.restname != null; } }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            if (this.evalbody == null)
                return this.body;

            IContext newcontext = this.closure;

            if (this.names != null && this.names.Count > 0)
            {
                newcontext = new Context(newcontext);
                for (int k = 0; k < this.names.Count; k++)
                    newcontext.SetValue(this.names[k], arguments[k]);
            }
            else if (this.restname != null)
                newcontext = new Context(newcontext);

            if (this.restname != null)
                newcontext.SetValue(this.restname, this.MakeList(this.arity, arguments));

            var result = this.evalbody.Evaluate(newcontext);

            while (result is RecurValues)
            {
                var rv = (RecurValues)result;
                newcontext = new Context(newcontext);
                int nelems = rv.Elements.Count;
                int nnames = this.names.Count;
                int k;

                for (k = 0; k < nelems && k < nnames; k++)
                    newcontext.SetValue(this.names[k], rv.Elements[k]);

                for (; k < nnames; k++)
                    newcontext.SetValue(this.names[k], null);

                result = this.evalbody.Evaluate(newcontext);
            }

            return result;
        }

        private List MakeList(int nelement, IList<object> elements)
        {
            if (nelement >= elements.Count)
                return null;

            return new List(elements[nelement], this.MakeList(nelement + 1, elements));
        }
    }
}
