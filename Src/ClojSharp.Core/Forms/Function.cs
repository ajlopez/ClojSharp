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
        private IList<string> argumentnames;
        private object body;
        private IEvaluable evalbody;

        public Function(Context closure, IList<string> argumentnames, object body)
        {
            this.closure = closure;
            this.argumentnames = argumentnames;
            this.body = body;

            if (body is IEvaluable)
                this.evalbody = (IEvaluable)body;
        }

        public override object EvaluateForm(Context context, IList<object> arguments)
        {
            if (this.evalbody != null)
                return this.evalbody.Evaluate(this.closure);

            return this.body;
        }
    }
}
