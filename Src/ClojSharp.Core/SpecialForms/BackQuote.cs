﻿namespace ClojSharp.Core.SpecialForms
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

            return this.Expand(arg, context);
        }

        private object Expand(object obj, IContext context)
        {
            if (obj is List)
                return this.Expand((List)obj, context);

            if (obj is Vector)
                return this.Expand((Vector)obj, context);

            return obj;
        }

        private object Expand(Vector vector, IContext context)
        {
            IList<object> elements = new List<object>();

            foreach (var element in vector.Elements)
                elements.Add(this.Expand(element, context));

            return new Vector(elements);
        }

        private object Expand(List list, IContext context)
        {
            var first = list.First;

            if (first is Symbol && ((Symbol)first).Name == "unquote")
                return context.GetValue(((Symbol)list.Next.First).Name);

            first = this.Expand(first, context);

            var next = this.Expand(list.Next, context);

            return new List(first, (ISeq)next);
        }
    }
}
