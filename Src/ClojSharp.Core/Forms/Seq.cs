namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class Seq : BaseUnaryForm
    {
        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            var arg = arguments[0];

            if (arg == null)
                return null;

            if (arg is Vector)
                return List.FromEnumerable(((Vector)arg).Elements);

            if (arg is EmptyList)
                return null;

            if (arg is List)
                return arg;

            return EnumerableSeq.MakeSeq((IEnumerable)arg);
        }
    }
}
