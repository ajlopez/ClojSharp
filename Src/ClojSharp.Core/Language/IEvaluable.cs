namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IEvaluable
    {
        object Evaluate(IContext context);
    }
}
