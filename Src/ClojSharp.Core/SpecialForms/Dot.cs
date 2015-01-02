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
                args = ((List)list.Next).ToList().Select(arg => Machine.Evaluate(arg, context)).ToArray();

            if (target is Type)
                return ((Type)target).InvokeMember(name, BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.InvokeMethod | BindingFlags.Static, null, target, args);
            else
                return type.InvokeMember(name, BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.InvokeMethod | BindingFlags.Instance, null, target, args);
        }
    }
}
