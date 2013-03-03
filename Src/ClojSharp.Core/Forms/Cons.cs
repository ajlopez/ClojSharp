namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;
    using ClojSharp.Core.Exceptions;

    public class Cons : BaseForm
    {
        public override int RequiredArity
        {
            get { return 2; }
        }

        public override bool VariableArity { get { return false; } }

        public override object EvaluateForm(Context context, IList<object> arguments)
        {
            if (arguments[1] != null && !(arguments[1] is ISeq))
                throw new ArgumentException(string.Format("Don't know how to create ISeq from {0}", arguments[1].GetType().FullName));

            return new List(arguments[0], (ISeq)arguments[1]);
        }
    }
}
