namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class Add : BaseForm
    {
        public override int RequiredArity
        {
            get { return 0; }
        }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            int result = 0;

            if (arguments != null)
                foreach (var value in arguments)
                    result += (int)value;

            return result;
        }
    }
}
