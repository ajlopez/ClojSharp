namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Subtract : BaseForm
    {
        public override object EvaluateForm(Context context, IList<object> arguments)
        {
            if (arguments.Count == 1)
                return -(int)arguments[0];

            return (int)arguments[0] - (int)arguments[1];
        }
    }
}
