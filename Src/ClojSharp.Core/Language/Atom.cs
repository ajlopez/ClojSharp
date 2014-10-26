namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Atom
    {
        private object value;

        public Atom(object value)
        {
            this.value = value;
        }

        public object Value { get { return this.value; } }
    }
}
