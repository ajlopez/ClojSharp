﻿namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class MapValue : IEvaluable
    {
        private IList<object> expressions;

        public MapValue(IList<object> expressions)
        {
            this.expressions = expressions;
        }

        public object Evaluate(Context context)
        {
            object[] values = new object[this.expressions.Count];

            for (var k = 0; k < values.Length; k++)
                values[k] = Machine.Evaluate(this.expressions[k], context);

            return new Map(values);
        }
    }
}
