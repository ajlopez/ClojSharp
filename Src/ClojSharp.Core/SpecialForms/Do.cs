namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;

    public class Do : IForm
    {
        public object Evaluate(Context context, IList<object> arguments)
        {
            object result = null;

            foreach (var argument in arguments)
                result = Machine.Evaluate(argument, context);

            return result;
        }
    }
}
