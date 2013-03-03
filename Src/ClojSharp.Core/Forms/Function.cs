namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Function : BaseForm
    {
        private Context closure;
        private IList<string> argumentnames;
        private object body;

        public Function(Context closure, IList<string> argumentnames, object body)
        {
            this.closure = closure;
            this.argumentnames = argumentnames;
            this.body = body;
        }

        public override object EvaluateForm(Context context, IList<object> arguments)
        {
            return this.body;
        }
    }
}
