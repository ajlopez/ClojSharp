namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Vector : IEvaluable, IObject
    {
        private IList<object> elements;
        private Map metadata;

        public Vector(IList<object> elements)
            : this(elements, null)
        {
        }

        internal Vector(IList<object> elements, Map metadata)
        {
            this.elements = elements;
            this.metadata = metadata;
        }

        public IList<object> Elements { get { return this.elements; } }

        public Map Metadata
        {
            get { return this.metadata; }
        }

        public object Evaluate(IContext context)
        {
            object[] values = new object[this.elements.Count];

            for (var k = 0; k < values.Length; k++)
                values[k] = Machine.Evaluate(this.elements[k], context);

            return new Vector(values, this.metadata);
        }

        public override string ToString()
        {
            string result = "[";

            for (int k = 0; k < this.elements.Count; k++)
            {
                var expr = this.elements[k];

                if (k > 0)
                    result += " ";

                result += expr.ToString();
            }

            return result + "]";
        }

        public IObject WithMetadata(Map map)
        {
            Map newmap = map;

            if (this.metadata != null)
                newmap = this.metadata.Merge(map);

            return new Vector(this.elements, newmap);
        }
    }
}
