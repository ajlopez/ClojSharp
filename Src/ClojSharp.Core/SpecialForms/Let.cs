namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using ClojSharp.Core.Exceptions;

    public class Let : IForm
    {
        private static IForm @do = new Do();

        public object Evaluate(Context context, IList<object> arguments)
        {
            var elements = ((VectorValue)arguments[0]).Expressions;

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
