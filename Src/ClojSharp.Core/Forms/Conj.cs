namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;

    public class Conj : BaseForm
    {
        public override int RequiredArity
        {
            get { return 2; }
        }

        public override bool VariableArity { get { return true; } }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            ISeq result = Language.List.AddItem((ISeq)arguments[0], arguments[1]);

            for (int k = 2; k < arguments.Count; k++)
                result = Language.List.AddItem(result, arguments[k]);

            return result;
        }
    }
}
