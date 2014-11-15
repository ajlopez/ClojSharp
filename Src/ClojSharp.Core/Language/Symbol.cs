namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Forms;

    public class Symbol : IEvaluable, IObject
    {
        private string name;
        private Map metadata;

        public Symbol(string name)
            : this(name, null)
        {
        }

        private Symbol(string name, Map metadata)
        {
            this.name = name;
            this.metadata = metadata;
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
            var result = context.GetValue(this.name);

            if (result == null && this.name[0] == '.')
            {
                result = new MethodForm(this.name.Substring(1));
                context.SetValue(this.name, result);
            }

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
