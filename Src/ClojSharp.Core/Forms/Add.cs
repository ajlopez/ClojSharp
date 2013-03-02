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
            int result = 0;

            if (arguments != null)
                foreach (var value in arguments)
                    result += (int)value;

            return result;
        }
    }
}
