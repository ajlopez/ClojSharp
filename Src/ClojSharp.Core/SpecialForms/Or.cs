namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;

    public class Or : IForm
    {
        public object Evaluate(IContext context, IList<object> arguments)
        {
            object result = null;

            for (int k = 0; k < arguments.Count; k++) 
            {
                result = Machine.Evaluate(arguments[k], context);

                if (Predicates.IsTrue(result))
                    return result;
            }

            return result;
        }
    }
}
