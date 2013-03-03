namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class Multiply : BaseForm
    {
        public override object EvaluateForm(Context context, IList<object> arguments)
        {
            int result = 1;

            if (arguments != null)
                foreach (var value in arguments)
                    result *= (int)value;

            return result;
        }
    }
}
