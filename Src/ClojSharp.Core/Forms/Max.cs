namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class Max : BaseForm
    {
        public override int RequiredArity
        {
            get { return 1; }
        }

        public override bool VariableArity { get { return true; } }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            var result = arguments[0];

            for (int k = 1; k < arguments.Count; k++)
                if (((IComparable)result).CompareTo(arguments[k]) < 0)
                    result = arguments[k];

            return result;
        }
    }
}
