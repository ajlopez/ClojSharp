namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using ClojSharp.Core.Language;

    public class MethodForm : BaseForm
    {
        private string name;

        public MethodForm(string name)
        {
            this.name = name;
        }

        public override int RequiredArity { get { return 1; } }

        public override bool VariableArity { get { return true; } }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            object arg = arguments[0];
            var type = arguments[0].GetType();

            object[] args;

            if (arguments.Count > 1)
                args = arguments.Skip(1).ToArray();
            else
                args = null;

            return type.InvokeMember(this.name, BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.GetProperty, null, arguments[0], args);
        }
    }
}
