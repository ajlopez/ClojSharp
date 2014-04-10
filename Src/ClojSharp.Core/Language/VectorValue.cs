namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class VectorValue : IEvaluable
    {
        private IList<object> expressions;

        public VectorValue(IList<object> expressions)
        {
            this.expressions = expressions;
        }

        public IList<object> Expressions { get { return this.expressions; } }

        public object Evaluate(IContext context)
        {
            object[] values = new object[this.expressions.Count];

            for (var k = 0; k < values.Length; k++)
                values[k] = Machine.Evaluate(this.expressions[k], context);

            return new Vector(values);
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
    }
}
