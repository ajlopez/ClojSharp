namespace ClojSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

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
            if (this.parent == null)
                this.values[name] = new Var(name, value);
            else
                this.values[name] = value;
        }

        public void SetVarValue(string name, object value)
        {
            if (this.parent != null)
                this.parent.SetVarValue(name, value);
            else
                this.SetValue(name, value);
        }

        public Var GetVar(string name)
        {
            if (this.parent != null)
                return this.parent.GetVar(name);

            if (this.values.ContainsKey(name))
                return (Var)this.values[name];

            return null;
        }

        public object GetValue(string name)
        {
            if (this.values.ContainsKey(name))
                if (this.parent == null)
                    return ((Var)this.values[name]).Value;
                else
                    return this.values[name];

            if (this.parent != null)
                return this.parent.GetValue(name);

            return null;
        }
    }
}
