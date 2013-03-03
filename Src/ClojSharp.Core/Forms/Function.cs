namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class Function : BaseForm
    {
        private Context closure;
        private IList<string> names;
        private object body;
        private IEvaluable evalbody;

        public Function(Context closure, IList<string> names, object body)
        {
            this.closure = closure;
            this.names = names;
            this.body = body;

            if (body is IEvaluable)
                this.evalbody = (IEvaluable)body;
        }

        public override object EvaluateForm(Context context, IList<object> arguments)
        {
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
