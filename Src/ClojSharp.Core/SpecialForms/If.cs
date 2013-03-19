namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Exceptions;

    public class If : IForm
    {
        public object Evaluate(Context context, IList<object> arguments)
        {
            if (arguments.Count < 2)
                throw new RuntimeException("Too few arguments to if");

            if (arguments.Count > 3)
                throw new RuntimeException("Too many arguments to if");

            var condition = arguments[0];

            var result = Machine.Evaluate(condition, context);

            if (!Machine.IsFalse(result))
                return Machine.Evaluate(arguments[1], context);

            if (arguments.Count > 2)
                return Machine.Evaluate(arguments[2], context);

            return null;
        }
    }
}
