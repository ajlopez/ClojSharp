namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;

    public class ToArray : BaseForm
    {
        public override int RequiredArity
        {
            get { return 1; }
        }

        public override bool VariableArity { get { return false; } }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            ISeq seq = (ISeq)arguments[0];

            IList<object> items = new List<object>();

            while (seq != null)
            {
                items.Add(seq.First);
                seq = seq.Next;
            }

            return items.ToArray();
        }
    }
}
