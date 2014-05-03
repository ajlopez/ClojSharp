namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class VectorValue : IEvaluable, IObject
    {
        private IList<object> expressions;
        private Map metadata;

        public VectorValue(IList<object> expressions)
            : this(expressions, null)
        {
        }

        private VectorValue(IList<object> expressions, Map metadata)
        {
            this.expressions = expressions;
            this.metadata = metadata;
        }

        public IList<object> Expressions { get { return this.expressions; } }

        public Map Metadata
        {
            get { return this.metadata; }
        }

        public object Evaluate(IContext context)
        {
            object[] values = new object[this.expressions.Count];

            for (var k = 0; k < values.Length; k++)
                values[k] = Machine.Evaluate(this.expressions[k], context);

            return new Vector(values, this.metadata);
        }

        public override string ToString()
        {
            string result = "[";

            for (int k = 0; k < this.expressions.Count; k++)
            {
                var expr = this.expressions[k];

                if (k > 0)
                    result += " ";

                result += expr.ToString();
            }

            return result + "]";
        }

        public IObject WithMetadata(Map map)
        {
            return new VectorValue(this.expressions, map);
        }
    }
}
