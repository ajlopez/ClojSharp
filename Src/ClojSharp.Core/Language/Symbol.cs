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
    }
}
