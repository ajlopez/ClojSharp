﻿namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;

    public abstract class BaseForm : IForm
    {
        public abstract int RequiredArity { get; }

        public virtual bool VariableArity { get { return true; } }

        public object Evaluate(IContext context, IList<object> arguments)
        {
            int arity = arguments == null ? 0 : arguments.Count;

            if (this.VariableArity) 
            {
                if (this.RequiredArity > arity)
                    throw new ArityException(this.GetType(), arity);
            }
            else if (this.RequiredArity != arity)
                throw new ArityException(this.GetType(), arity);

            if (arguments == null)
                return this.EvaluateForm(context, null);

            for (var k = 0; k < arguments.Count; k++)
                arguments[k] = Machine.Evaluate(arguments[k], context);

            return this.EvaluateForm(context, arguments);
        }

        public abstract object EvaluateForm(IContext context, IList<object> arguments);
    }
}
