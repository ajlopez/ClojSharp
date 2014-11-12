namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;

    public class Macro : IForm
    {
        private IContext closure;
        private IList<string> names;
        private string restname;
        private object body;

        public Macro(IContext closure, IList<string> names, string restname, object body)
        {
            this.closure = closure;
            this.names = names;
            this.restname = restname;
            this.body = body;
        }

        public object Evaluate(IContext context, IList<object> arguments)
        {
            IContext newcontext = new Context(this.closure);

            if (this.names != null && this.names.Count > 0)
                for (int k = 0; k < this.names.Count; k++)
                    newcontext.SetValue(this.names[k], arguments[k]);

            if (this.restname != null)
                newcontext.SetValue(this.restname, this.MakeList(this.names != null ? this.names.Count : 0, arguments));

            return Machine.Evaluate(Machine.Evaluate(this.body, newcontext), context);
        }

        private List MakeList(int nelement, IList<object> elements)
        {
            if (nelement >= elements.Count)
                return null;

            return new List(elements[nelement], this.MakeList(nelement + 1, elements));
        }
    }
}
