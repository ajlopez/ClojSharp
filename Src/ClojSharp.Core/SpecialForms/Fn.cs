namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;

    public class Fn : IForm
    {
        public object Evaluate(IContext context, IList<object> arguments)
        {
            if (arguments[0] is List)
            {
                IList<Function> functions = new List<Function>();

                foreach (var arg in arguments)
                    functions.Add(this.EvaluateFunction(context, ((List)arg).ToList()));

                return new MultiFunction(functions);
            }
            else
                return this.EvaluateFunction(context, arguments);
        }

        private Function EvaluateFunction(IContext context, IList<object> arguments)
        {
            var elements = ((Vector)arguments[0]).Elements;
            IList<string> names = new List<string>();
            string restname = null;
            int nelement = 0;

            if (elements != null)
                foreach (var element in elements)
                {
                    string name = ((Symbol)element).Name;

                    if (name == "&" && nelement == elements.Count - 2)
                    {
                        restname = ((Symbol)elements[elements.Count - 1]).Name;
                        break;
                    }

                    names.Add(((Symbol)element).Name);
                    nelement++;
                }

            object body = arguments[1];

            return new Function(context, names, restname, body);
        }
    }
}
