namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;

    public class Loop : IForm
    {
        private static IForm @do = new Do();

        public object Evaluate(IContext context, IList<object> arguments)
        {
            if (arguments.Count == 0)
                throw new ArityException(typeof(Loop), arguments.Count);

            var vector = arguments[0] as Vector;

            if (vector == null)
                throw new IllegalArgumentException("loop requires a vector for its bindings");

            var elements = vector.Elements;

            if (elements.Count % 2 != 0)
                throw new IllegalArgumentException("loop requires an even number of forms in binding vector");

            IList<string> names = new List<string>();

            var newcontext = new Context(context);

            for (var k = 0; k < elements.Count; k += 2)
            {
                var symbol = (Symbol)elements[k];
                newcontext.SetValue(symbol.Name, Machine.Evaluate(elements[k + 1], newcontext));
            }

            var body = arguments.Skip(1).ToList();
            object result = null;

            for (result = @do.Evaluate(newcontext, body); result is RecurValues; result = @do.Evaluate(newcontext, body))
            {
                var values = ((RecurValues)result).Elements;
                newcontext = new Context(context);

                for (var k = 0; k < values.Count; k++)
                {
                    var symbol = (Symbol)elements[k * 2];
                    newcontext.SetValue(symbol.Name, values[k]);
                }
            }

            return result;
        }
    }
}
