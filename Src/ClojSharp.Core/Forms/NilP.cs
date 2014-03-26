namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class NilP : BaseForm
    {
        public override int RequiredArity { get { return 1; } }

        public override bool VariableArity { get { return false; } }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            return Predicates.IsNil(arguments[0]);
        }
    }
}
