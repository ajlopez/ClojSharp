namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;

    public class MFn : IForm
    {
        public object Evaluate(IContext context, IList<object> arguments)
        {
            return this.EvaluateMacro(context, arguments);
        }

        private Macro EvaluateMacro(IContext context, IList<object> arguments)
        {
            var elements = ((VectorValue)arguments[0]).Expressions;
            IList<string> names = new List<string>();
            int nelement = 0;

            foreach (var element in elements)
            {
                string name = ((Symbol)element).Name;

                names.Add(((Symbol)element).Name);
                nelement++;
            }

            object body = arguments[1];

            return new Macro(context, names, body);
        }
    }
}
