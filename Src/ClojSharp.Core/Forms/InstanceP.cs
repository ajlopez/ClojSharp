namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;

    public class InstanceP : BaseForm
    {
        public override int RequiredArity
        {
            get { return 2; }
        }

        public override bool VariableArity { get { return false; } }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            Type type = (Type)arguments[0];
            object arg = arguments[1];

            return type.IsInstanceOfType(arg);
        }
    }
}
