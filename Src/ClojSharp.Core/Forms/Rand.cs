namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;

    public class Rand : BaseForm
    {
        private static Random random = new Random();

        public override int RequiredArity { get { return 0; } }

        public override bool VariableArity { get { return true; } }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            int arity = arguments == null ? 0 : arguments.Count;

            if (arity != 0 && arity != 1)
                throw new ArityException(this.GetType(), arity);

            if (arity == 0)
                return random.NextDouble();

            if (Predicates.IsReal(arguments[0]))
                return random.NextDouble() * Convert.ToDouble(arguments[0]);

            return random.NextDouble() * (int)arguments[0];
        }
    }
}
