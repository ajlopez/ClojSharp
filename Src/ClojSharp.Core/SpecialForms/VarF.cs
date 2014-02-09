namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using ClojSharp.Core.Exceptions;

    public class VarF : IForm
    {
        public object Evaluate(IContext context, IList<object> arguments)
        {
            Symbol symbol = (Symbol)arguments[0];

            var var = context.TopContext.GetVar(symbol.Name);

            if (var == null)
                throw new RuntimeException(string.Format("Unable to resolve var: {0} in this context", symbol.Name));

            return var;
        }
    }
}
