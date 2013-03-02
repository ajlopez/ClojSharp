namespace ClojSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Context
    {
        private IDictionary<string, object> values = new Dictionary<string, object>();
        private Context parent;

        public Context()
            : this(null)
        {
        }

        public Context(Context parent)
        {
            this.parent = parent;
        }

        public void SetValue(string name, object value)
        {
            values[name] = value;
        }

        public object GetValue(string name)
        {
            if (values.ContainsKey(name))
                return values[name];

            if (parent != null)
                return parent.GetValue(name);

            return null;
        }
    }
}
