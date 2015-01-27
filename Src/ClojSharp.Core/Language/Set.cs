namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;

    public class Set : IEvaluable, IMetadata
    {
        private HashSet<object> keys;
        private Set set;
        private Map metadata;

        public Set(IList<object> keys)
            : this(keys, null, null)
        {
        }

        internal Set(IList<object> keys, Set set, Map metadata)
        {
            this.metadata = metadata;
            this.set = set;

            if (keys == null)
                this.keys = new HashSet<object>();
            else
                this.keys = new HashSet<object>(keys);
        }

        public Map Metadata
        {
            get { return this.metadata; }
        }

        public static Set Create(IList<object> keys)
        {
            return new Set(keys);
        }

        public bool HasKey(object key)
        {
            if (this.keys.Contains(key))
                return true;

            if (this.set != null)
                return this.set.HasKey(key);

            return false;
        }

        public Set Add(object key)
        {
            if (this.HasKey(key))
                return this;

            return new Set(new object[] { key }, this, this.metadata);
        }

        public Set Remove(object key)
        {
            if (!this.HasKey(key))
                return this;

            return new Set(this.keys.Where(k => k != key && (k == null || !k.Equals(key))).ToList());
        }

        public object Evaluate(IContext context)
        {
            IList<object> values = new List<object>();

            foreach (var expr in this.keys)
                values.Add(Machine.Evaluate(expr, context));

            return new Set(values);
        }

        public override string ToString()
        {
            string result = "#{";

            if (this.set != null)
            {
                var txt = this.set.ToString();
                result += txt.Substring(2, txt.Length - 3);
            }

            foreach (var key in this.keys)
            {
                if (result.Length > 2)
                    result += " ";

                result += Machine.ToString(key);
            }

            return result + "}";
        }
    }
}
