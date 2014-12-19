namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;

    public class Dot : IForm
    {
        public object Evaluate(IContext context, IList<object> arguments)
        {
            var target = Machine.Evaluate(arguments[0], context);
            var type = target.GetType();
            var list = (List)arguments[1];
            var name = ((Symbol)list.First).Name;

            object[] args = null;

            if (list.Next != null)
                args = ((List)list.Next).ToList().ToArray();

            return type.InvokeMember(name, BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Instance, null, target, args);
        }
    }
}
