namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;

    public class List
    {
        private object first;
        private object rest;

        public List(object first, object rest)
        {
            this.first = first;
            this.rest = rest;
        }

        public object First { get { return this.first; } }

        public object Rest { get { return this.rest; } }

        public object Evaluate(Context context)
        {
            var symbol = (Symbol)this.first;
            var fn = (Add)context.GetValue(symbol.Name);

            IList<object> arguments = new List<object>();

            for (var args = (List)this.rest; args != null; args = (List)args.rest)
                arguments.Add(args.first);

            return fn.Evaluate(context, arguments);
        }
    }
}
