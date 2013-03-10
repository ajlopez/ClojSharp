namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Symbol : IEvaluable
    {
        private string name;

        public Symbol(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

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
