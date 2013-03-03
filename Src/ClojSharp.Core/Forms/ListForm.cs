namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class ListForm : BaseForm
    {
        public override int RequiredArity { get { return 0; } }

        public override object EvaluateForm(Context context, IList<object> arguments)
        {
            List result = null;
            for (var k = arguments.Count; k-- > 0; )
                result = new List(arguments[k], result);
            return result;
        }
    }
}
