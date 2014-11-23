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
            var argument = arguments[0];

            if (argument is VectorValue)
                return new Vector(((VectorValue)argument).Expressions);

            return argument;
        }
    }
}
