namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Var
    {
        private string @namespace;
        private string name;
        private object value;

        public Var(string name)
            : this("user", name, null)
        {
        }

        public Var(string @namespace, string name, object value)
        {
            this.@namespace = @namespace;
            this.name = name;
            this.value = value;
        }

        public string Name { get { return this.name; } }

        public string FullName { get { return string.Format("{0}/{1}", this.@namespace, this.name); } }

        public object Value { get { return this.value; } }
    }
}
