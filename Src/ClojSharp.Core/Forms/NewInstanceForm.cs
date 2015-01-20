namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using ClojSharp.Core.Language;

    public class NewInstanceForm : BaseForm
    {
        private Type type;

        public NewInstanceForm(string typename)
        {
            this.type = Type.GetType(typename);
        }

        public override int RequiredArity { get { return 0; } }

        public override bool VariableArity { get { return true; } }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            object[] args = null;

            if (arguments != null && arguments.Count > 0)
                args = arguments.ToArray();

            return Activator.CreateInstance(this.type, args);
        }
    }
}
