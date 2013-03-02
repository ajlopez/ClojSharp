namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class Def
    {
        public object Evaluate(Context context, IList<object> arguments)
        {
            Symbol symbol = (Symbol)arguments[0];
            object value = arguments[1];

            if (value is Symbol)
                value = context.GetValue(((Symbol)value).Name);

            context.SetRootValue(symbol.Name, value);
            return new Var(symbol.Name);
        }
    }
}
