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
        public object Evaluate(Context context, IList<object> arguments)
        {
            var elements = ((VectorValue)arguments[0]).Expressions;
            IList<string> names = new List<string>();

            foreach (var element in elements)
                names.Add(((Symbol)element).Name);

            object body = arguments[1];

            return new Function(context, names, body);
        }
    }
}
