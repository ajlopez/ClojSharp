namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;

    public class Quote : IForm
    {
        public object Evaluate(IContext context, IList<object> arguments)
        {
            return arguments[0];
        }
    }
}
