namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Add
    {
        public object Evaluate(Context context, IList<object> arguments)
        {
            return (int)arguments[0] + (int)arguments[1];
        }
    }
}
