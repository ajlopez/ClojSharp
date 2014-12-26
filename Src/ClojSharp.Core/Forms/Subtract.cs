namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;

    public class Subtract : BaseForm
    {
        public override int RequiredArity
        {
            get { return 1; }
        }

        public override bool VariableArity
        {
            get { return true; }
        }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            if (arguments.Any(arg => Predicates.IsReal(arg)))
            {
                if (arguments.Count == 1)
                    return -Convert.ToDouble(arguments[0]);

                double result = Convert.ToDouble(arguments[0]);

                for (var k = 1; k < arguments.Count; k++)
                    result -= Convert.ToDouble(arguments[k]);

                return result;
            }
            else
            {
                if (arguments.Count == 1)
                    return -(int)arguments[0];

                int result = (int)arguments[0];

                for (var k = 1; k < arguments.Count; k++)
                    result -= (int)arguments[k];

                return result;
            }
        }
    }
}
