namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class Str : BaseForm
    {
        public override int RequiredArity { get { return 0; } }

        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            string result = string.Empty;

            foreach (var argument in arguments)
                if (argument != null)
                    result += argument.ToString();

            return result;
        }
    }
}
