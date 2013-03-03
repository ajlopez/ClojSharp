namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;
    using ClojSharp.Core.Exceptions;

    public class Function : BaseForm
    {
        private Context closure;
        private IList<string> names;
        private object body;
        private IEvaluable evalbody;
        private int arity;

        public Function(Context closure, IList<string> names, object body)
        {
            this.closure = closure;
            this.names = names;
            this.body = body;
            this.arity = names == null ? 0 : names.Count;

            if (body is IEvaluable)
                this.evalbody = (IEvaluable)body;
        }

        public override object EvaluateForm(Context context, IList<object> arguments)
        {
            int arity = arguments == null ? 0 : arguments.Count;

            if (this.arity != arity)
                throw new ArityException(typeof(Function), arity);

            if (this.evalbody == null)
                return this.body;

            Context newcontext = this.closure;

            if (this.names != null && this.names.Count > 0) 
            {
                newcontext = new Context(newcontext);
                for (int k = 0; k < this.names.Count; k++)
                    newcontext.SetValue(this.names[k], arguments[k]);
            }

            return this.evalbody.Evaluate(newcontext);
        }
    }
}
