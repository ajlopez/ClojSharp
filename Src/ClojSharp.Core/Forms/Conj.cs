namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;

    public class Conj : BaseForm
    {
        public override int RequiredArity
        {
            get { return 2; }
        }

        public override bool VariableArity { get { return true; } }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            var first = arguments[0];

            if (first == null || first is List)
            {
                ISeq result = new List(arguments[1], (ISeq)arguments[0]);

                for (int k = 2; k < arguments.Count; k++)
                    result = new List(arguments[k], result);

                return result;
            }

            if (first is Set)
            {
                Set set = ((Set)first).Add(arguments[1]);

                for (int k = 2; k < arguments.Count; k++)
                    set = set.Add(arguments[k]);

                return set;
            }

            Vector vector = Language.Vector.AddItem((Vector)first, arguments[1]);

            for (int k = 2; k < arguments.Count; k++)
                vector = Language.Vector.AddItem(vector, arguments[k]);

            return vector;
        }
    }
}
