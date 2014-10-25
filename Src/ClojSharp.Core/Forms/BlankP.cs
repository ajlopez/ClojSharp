namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class BlankP : BaseUnaryForm
    {
        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            var arg = arguments[0];

            if (arg == null)
                return true;

            if (arg is string)
                return string.IsNullOrWhiteSpace((string)arg);

            return false;
        }
    }
}
