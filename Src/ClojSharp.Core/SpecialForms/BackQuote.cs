namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;

    public class BackQuote : IForm
    {
        public object Evaluate(IContext context, IList<object> arguments)
        {
            var arg = arguments[0];

            if (arg is List)
                return Expand((List)arg, context);

            return arg;
        }

        private object Expand(List list, IContext context)
        {
            var first = list.First;

            if (first is Symbol && ((Symbol)first).Name == "unquote")
                return context.GetValue(((Symbol)list.Next.First).Name);

            if (first is List)
                first = Expand((List)first, context);

            object next = list.Next;

            if (next != null && next is List)
                next = Expand((List)next, context);

            return new List(first, (ISeq)next);
        }
    }
}
