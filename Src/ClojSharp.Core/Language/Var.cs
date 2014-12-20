namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Var : IObject, IReference
    {
        private static Keyword macrokw = new Keyword("macro");

        private Namespace @namespace;
        private string name;
        private object value;
        private Map metadata;
        private bool ismacro;

        public Var(Machine machine, string name)
            : this(machine.GetNamespace("user"), name, null)
        {
        }

        public Var(Machine machine, string name, object value)
            : this(machine.GetNamespace("user"), name, value)
        {
        }

        public Var(Namespace @namespace, string name, object value)
            : this(@namespace, name, value, null)
        {
        }

        private Var(Namespace @namespace, string name, object value, Map metadata)
        {
            this.@namespace = @namespace;
            this.name = name;
            this.value = value;

            this.metadata = metadata;

            if (metadata != null && true.Equals(metadata.GetValue(macrokw)))
                this.ismacro = true;
            else
                this.ismacro = false;
        }

        public string Name { get { return this.name; } }

        public string FullName { get { return string.Format("{0}/{1}", this.@namespace.Name, this.name); } }

        public object Value { get { return this.value; } }

        public Map Metadata { get { return this.metadata; } }

        public bool IsMacro { get { return this.ismacro; } }

        public IObject WithMetadata(Map map)
        {
            return new Var(this.@namespace, this.name, this.value, map);
        }
    }
}
