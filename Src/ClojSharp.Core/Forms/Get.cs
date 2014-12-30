namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;

    public class Get : BaseForm
    {
        public override int RequiredArity
        {
            get { return 2; }
        }

        public override bool VariableArity { get { return true; } }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            if (arguments[0] is Vector)
            {
                Vector vector = (Vector)arguments[0];
                int index = Convert.ToInt32(arguments[1]);

                if (index < 0 || index > vector.Elements.Count)
                    if (arguments.Count > 2)
                        return arguments[2];
                    else
                        return null;

                return vector.Elements[index];
            }

            Map map = (Map)arguments[0];
            
            if (!map.HasValue(arguments[1]))
                if (arguments.Count > 2)
                    return arguments[2];
                else
                    return null;

            return map.GetValue(arguments[1]);
        }
    }
}
