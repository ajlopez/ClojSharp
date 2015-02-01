namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class Multiply : BaseForm
    {
        public override int RequiredArity
        {
            get { return 0; }
        }

        public override bool VariableArity { get { return true; } }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            if (arguments != null)
                if (arguments.Any(arg => Predicates.IsReal(arg)))
                {
                    double result = 1.0;

                    foreach (var value in arguments)
                        result *= Convert.ToDouble(value);

                    return result;
                }
                else
                {
                    long result = 1;

                    foreach (var value in arguments)
                        result *= Convert.ToInt64(value);

                    if (result <= int.MaxValue && result >= int.MinValue)
                        return (int)result;

                    return result;
                }

            return 1;
        }
    }
}
