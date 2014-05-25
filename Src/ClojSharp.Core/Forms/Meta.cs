namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class Meta : BaseUnaryForm
    {
        public override object EvaluateForm(IContext context, IList<object> arguments)
        {
            return ((IMetadata)arguments[0]).Metadata;
        }
    }
}
