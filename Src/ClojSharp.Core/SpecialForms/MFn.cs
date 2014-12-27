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
            if (arguments[0] is List)
            {
                IList<Macro> macros = new List<Macro>();

                foreach (var arg in arguments)
                    macros.Add(this.EvaluateMacro(context, ((List)arg).ToList()));

                return new MultiMacro(macros);
            }
            else
                return this.EvaluateMacro(context, arguments);
        }

        private Macro EvaluateMacro(IContext context, IList<object> arguments)
        {
            var elements = ((Vector)arguments[0]).Elements;
            IList<string> names = new List<string>();
            string restname = null;
            int nelement = 0;

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

            return new Macro(context, names, restname, body);
        }
    }
}
