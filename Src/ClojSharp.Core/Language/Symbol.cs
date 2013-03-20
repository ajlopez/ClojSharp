namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;

    public class Symbol : IEvaluable, IMeta
    {
        private string name;
        private Map metadata;

        public Symbol(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public Map Metadata
        {
            get
            {
                return this.metadata;
            }

            set
            {
                if (this.metadata != null)
                    throw new RuntimeException("metadata already set");

                this.metadata = value;
            }
        }

        public object Evaluate(Context context)
        {
            return context.GetValue(this.name);
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
    }
}
