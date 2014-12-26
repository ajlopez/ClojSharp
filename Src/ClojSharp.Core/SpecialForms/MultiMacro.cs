namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;

    public class MultiMacro : BaseMacro
    {
        private IList<Macro> macros;

        public MultiMacro(IList<Macro> macros)
        {
            this.macros = macros;
        }

        public override int RequiredArity
        {
            get { return this.macros.Min(f => f.RequiredArity); }
        }

        public override bool VariableArity { get { return this.macros.Count > 1 || this.macros[0].VariableArity; } }

        public override object EvaluateMacro(IContext context, IList<object> arguments)
        {
            int arity = arguments == null ? 0 : arguments.Count;

            foreach (var fn in this.macros)
                if (fn.RequiredArity == arity)
                    return fn.Evaluate(context, arguments);
                else if (fn.RequiredArity < arity && fn.VariableArity)
                    return fn.Evaluate(context, arguments);

            throw new ArityException(this.GetType(), arity);
        }

        public override object Expand(IList<object> arguments)
        {
            int arity = arguments == null ? 0 : arguments.Count;

            foreach (var fn in this.macros)
                if (fn.RequiredArity == arity)
                    return fn.Expand(arguments);
                else if (fn.RequiredArity < arity && fn.VariableArity)
                    return fn.Expand(arguments);

            throw new ArityException(this.GetType(), arity);
        }
    }
}
