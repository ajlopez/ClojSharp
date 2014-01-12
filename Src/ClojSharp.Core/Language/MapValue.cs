namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;

    public class MapValue : IEvaluable
    {
        private IList<object> expressions;

        public MapValue(IList<object> expressions)
        {
            this.expressions = expressions;
        }

        public object Evaluate(Context context)
        {
            if (this.expressions.Count % 2 != 0)
                throw new RuntimeException("Map literal must contain an even number of forms");

            object[] values = new object[this.expressions.Count];

            for (var k = 0; k < values.Length; k++)
                values[k] = Machine.Evaluate(this.expressions[k], context);

            return new Map(values);
        }
    }
}
