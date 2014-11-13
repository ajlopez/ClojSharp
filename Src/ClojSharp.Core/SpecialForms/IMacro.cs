namespace ClojSharp.Core.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;

    public interface IMacro: IForm
    {
        object Expand(IList<object> arguments);
    }
}
