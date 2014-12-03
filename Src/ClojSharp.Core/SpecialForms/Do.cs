namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;

    public class Do : IForm
    {
        public object Evaluate(IContext context, IList<object> arguments)
        {
            object result = null;

            for (int k = 0; k < arguments.Count; k++)
            {
                arguments[k] = Machine.CompileExpression(arguments[k], context);
                result = Machine.Evaluate(arguments[k], context);
            }

            return result;
        }
    }
}
