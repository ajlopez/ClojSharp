namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;

    public class Divide : BaseForm
    {
        public override object EvaluateForm(Context context, IList<object> arguments)
        {
            if (arguments.Count == 0)
                throw new ArityException(typeof(Divide), 0);

            if (arguments.Count == 1)
                return (int)arguments[0];

            var result = (int)arguments[0];

            for (var k = 1; k < arguments.Count; k++)
                result /= (int)arguments[k];

            return result;
        }
    }
}
