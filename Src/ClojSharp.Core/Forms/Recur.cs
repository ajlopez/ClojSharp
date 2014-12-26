namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class Recur : BaseForm
    {
        public override int RequiredArity { get { return 0; } }

        public override bool VariableArity { get { return true; } }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            if (arguments == null || arguments.Count == 0)
                return new RecurValues(new List<object>());

            return new RecurValues(arguments);
        }
    }
}
