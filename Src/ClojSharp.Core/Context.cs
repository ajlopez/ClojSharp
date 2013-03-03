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
            this.values[name] = value;
        }

        public void SetRootValue(string name, object value)
        {
            if (this.parent != null)
                this.parent.SetRootValue(name, value);
            else
                this.SetValue(name, value);
        }

        public object GetValue(string name)
        {
            if (this.values.ContainsKey(name))
                return this.values[name];

            if (this.parent != null)
                return this.parent.GetValue(name);

            return null;
        }
    }
}
