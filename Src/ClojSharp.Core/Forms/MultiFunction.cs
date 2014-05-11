namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;

    public class MultiFunction : BaseForm
    {
        private IList<Function> functions;

        public MultiFunction(IList<Function> functions)
        {
            this.functions = functions;
        }

        public override int RequiredArity
        {
            get { return this.functions.Min(f => f.RequiredArity); }
        }

        public override bool VariableArity { get { return this.functions.Count > 1 || this.functions[0].VariableArity; } }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            int arity = arguments == null ? 0 : arguments.Count;

            foreach (var fn in this.functions)
                if (fn.RequiredArity == arity)
                    return fn.Evaluate(context, arguments);
                else if (fn.RequiredArity < arity && fn.VariableArity)
                    return fn.Evaluate(context, arguments);

            throw new ArityException(this.GetType(), arity);
        }
    }
}
