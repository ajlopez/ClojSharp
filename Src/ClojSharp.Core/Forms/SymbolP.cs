namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class SymbolP : BaseUnaryForm
    {
        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            return arguments[0] is Symbol;
        }
    }
}
