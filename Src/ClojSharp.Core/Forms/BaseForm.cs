namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public abstract class BaseForm : IForm
    {
        public object Evaluate(Context context, IList<object> arguments)
        {
            if (arguments == null)
                return this.EvaluateForm(context, null);

            for (var k = 0; k < arguments.Count; k++)
                if (arguments[k] is IEvaluable)
                    arguments[k] = ((IEvaluable)arguments[k]).Evaluate(context);

            return this.EvaluateForm(context, arguments);
        }

        public abstract object EvaluateForm(Context context, IList<object> arguments);
    }
}
