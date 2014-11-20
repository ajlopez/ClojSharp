namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;

    public class New : IForm
    {
        public object Evaluate(IContext context, IList<object> arguments)
        {
            Type type = Type.GetType(((Symbol)arguments[0]).Name);
            var args = new List<object>();

            foreach (var argument in arguments.Skip(1))
                args.Add(Machine.Evaluate(argument, context));

            return Activator.CreateInstance(type, args.ToArray());
        }
    }
}
