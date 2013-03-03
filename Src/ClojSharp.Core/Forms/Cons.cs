namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class Cons : BaseForm
    {
        public override object EvaluateForm(Context context, IList<object> arguments)
        {
            return new List(arguments[0], (ISeq)arguments[1]);
        }
    }
}
