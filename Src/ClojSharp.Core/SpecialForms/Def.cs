﻿namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;

    public class Def : IForm
    {
        public object Evaluate(Context context, IList<object> arguments)
        {
            Symbol symbol = (Symbol)arguments[0];
            object value = null;

            if (arguments.Count == 2) 
            {
                value = arguments[1];

                if (value is IEvaluable)
                    value = ((IEvaluable)value).Evaluate(context);
            }

            context.SetVarValue(symbol.Name, value);

            return context.GetVar(symbol.Name);
        }
    }
}
