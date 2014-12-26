namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;

    public abstract class BaseMacro : IMacro
    {
        public abstract int RequiredArity { get; }

        public abstract bool VariableArity { get; }

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

            return this.EvaluateMacro(context, arguments);
        }

        public abstract object EvaluateMacro(IContext context, IList<object> arguments);

        public abstract object Expand(IList<object> arguments);
    }
}
