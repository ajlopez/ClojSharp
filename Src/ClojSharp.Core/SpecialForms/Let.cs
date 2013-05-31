namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;

    public class Let : IForm
    {
        private static IForm @do = new Do();

        public object Evaluate(Context context, IList<object> arguments)
        {
            if (arguments.Count == 0)
                throw new ArityException(typeof(Let), arguments.Count);

            var vector = arguments[0] as VectorValue;

            if (vector == null)
                throw new IllegalArgumentException("let requires a vector for its bindings");

            var elements = vector.Expressions;

            if (elements.Count % 2 != 0)
                throw new IllegalArgumentException("let requires an even number of forms in binding vector");

            IList<string> names = new List<string>();

            var newcontext = new Context(context);

            for (var k = 0; k < elements.Count; k += 2)
            {
                var symbol = (Symbol)elements[k];
                newcontext.SetValue(symbol.Name, Machine.Evaluate(elements[k + 1], newcontext));
            }

            return @do.Evaluate(newcontext, arguments.Skip(1).ToList());
        }
    }
}
