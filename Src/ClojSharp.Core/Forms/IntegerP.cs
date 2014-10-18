namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class IntegerP : BaseUnaryForm
    {
        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            return Predicates.IsInteger(arguments[0]);
        }
    }
}
