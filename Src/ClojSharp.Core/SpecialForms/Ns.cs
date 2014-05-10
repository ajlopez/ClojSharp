namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;

    public class Ns : IForm
    {
        public object Evaluate(IContext context, IList<object> arguments)
        {
            Symbol symbol = (Symbol)arguments[0];

            var ns = new Namespace(symbol.Name);

            context.TopContext.SetValue("*ns*", ns);

            return ns;
        }
    }
}
