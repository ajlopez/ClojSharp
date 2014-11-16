namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Forms;

    public class Symbol : IEvaluable, IObject
    {
        private string name;
        private Map metadata;
        private object value;
        private bool hasns;

        public Symbol(string name)
            : this(name, null)
        {
        }

        private Symbol(string name, Map metadata)
        {
            this.name = name;
            this.metadata = metadata;

            if (name[0] == '.')
                this.value = new MethodForm(this.name.Substring(1));

            this.hasns = name.IndexOf('/') > 0;
        }

        public string Name { get { return this.name; } }

        public Map Metadata
        {
            get
            {
                return this.metadata;
            }
        }

        public object Evaluate(IContext context)
        {
            if (this.name[0] == '.')
                return this.value;

            if (this.hasns)
            {
                var words = this.name.Split('/');
                var type = Type.GetType(words[0]);
                var name = words[1];
                return type.InvokeMember(name, BindingFlags.Public | BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.Static, null, type, null);
            }

            var result = context.GetValue(this.name);

            return result;
        }

        public override string ToString()
        {
            return this.name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is Symbol)
            {
                var symbol = (Symbol)obj;

                return this.Name.Equals(symbol.Name);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() + 17;
        }

        public IObject WithMetadata(Map map)
        {
            return new Symbol(this.name, map);
        }
    }
}
