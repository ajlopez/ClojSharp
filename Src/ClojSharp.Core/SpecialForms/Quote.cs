namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;

    public class Quote : IForm
    {
        public object Evaluate(IContext context, IList<object> arguments)
        {
            return arguments[0];
        }
    }
}
