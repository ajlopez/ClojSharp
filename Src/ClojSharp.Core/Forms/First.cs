namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class First : BaseForm
    {
        public override int RequiredArity { get { return 1; } }

        public override bool VariableArity { get { return false; } }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            if (arguments[0] == null)
                return null;

            if (!(arguments[0] is ISeq))
                throw new ArgumentException(string.Format("Don't know how to create ISeq from {0}", arguments[0].GetType().FullName));

            return ((ISeq)arguments[0]).First;
        }
    }
}
