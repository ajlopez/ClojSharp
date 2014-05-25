﻿namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Var : IObject
    {
        private string @namespace;
        private string name;
        private object value;
        private Map metadata;

        public Var(string name)
            : this("user", name, null)
        {
        }

        public Var(string name, object value)
            : this("user", name, value)
        {
        }

        public Var(string @namespace, string name, object value)
            : this(@namespace, name, value, null)
        {
        }

        private Var(string @namespace, string name, object value, Map metadata)
        {
            this.@namespace = @namespace;
            this.name = name;
            this.value = value;
            this.metadata = metadata;
        }

        public string Name { get { return this.name; } }

        public string FullName { get { return string.Format("{0}/{1}", this.@namespace, this.name); } }

        public object Value { get { return this.value; } }

        public IObject WithMetadata(Map map)
        {
            return new Var(this.@namespace, this.name, this.value, map);
        }

        public Map Metadata { get { return this.metadata; } }
    }
}
