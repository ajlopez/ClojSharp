namespace ClojSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;

    public class VarContext : IContext
    {
        private IDictionary<string, Var> variables = new Dictionary<string, Var>();

        public VarContext TopContext { get { return this; } }

        public object GetValue(string name)
        {
            if (this.variables.ContainsKey(name))
                return this.variables[name].Value;

            return null;
        }

        public void SetValue(string name, object value)
        {
            this.variables[name] = new Var(name, value);
        }

        public Var GetVar(string name)
        {
            if (this.variables.ContainsKey(name))
                return this.variables[name];

            return null;
        }
    }
}
