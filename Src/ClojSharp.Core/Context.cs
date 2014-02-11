namespace ClojSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class Context : ClojSharp.Core.IContext
    {
        private IDictionary<string, object> values = new Dictionary<string, object>();
        private IContext parent;
        private VarContext topcontext;

        public Context()
            : this(null)
        {
        }

        public Context(IContext parent)
        {
            this.parent = parent;

            if (parent != null)
                this.topcontext = parent.TopContext;
        }

        public VarContext TopContext { get { return this.topcontext; } }

        public void SetValue(string name, object value)
        {
            if (this.parent == null)
                this.values[name] = new Var(name, value);
            else
                this.values[name] = value;
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
