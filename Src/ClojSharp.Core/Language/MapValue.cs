namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;

    public class MapValue : IEvaluable, IObj
    {
        private IList<object> expressions;
        private Map metadata;

        public MapValue(IList<object> expressions)
            : this(expressions, null)
        {
        }

        private MapValue(IList<object> expressions, Map metadata)
        {
            this.expressions = expressions;
            this.metadata = metadata;
        }

        public Map Metadata
        {
            get { return this.metadata; }
        }

        public object Evaluate(IContext context)
        {
            if (this.expressions.Count % 2 != 0)
                throw new RuntimeException("Map literal must contain an even number of forms");

            object[] values = new object[this.expressions.Count];

            for (var k = 0; k < values.Length; k++)
                values[k] = Machine.Evaluate(this.expressions[k], context);

            return new Map(values, this.metadata);
        }

        public override string ToString()
        {
            string result = "{";

            for (int k = 0; k < this.expressions.Count; k++)
            {
                var expr = this.expressions[k];

                if (k > 0)
                    result += " ";

                result += expr.ToString();
            }

            return result + "}";
        }

        public IObj WithMeta(Map map)
        {
            return new MapValue(this.expressions, map);
        }
    }
}
