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

        public override bool VariableArity { get { return false; } }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            Vector vector = (Vector)arguments[0];
            int index = Convert.ToInt32(arguments[1]);

            if (index < 0 || index > vector.Elements.Count)
                return null;

            return vector.Elements[index];
        }
    }
}
