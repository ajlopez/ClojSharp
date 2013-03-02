namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class List
    {
        private object first;
        private object rest;

        public List(object first, object rest)
        {
            this.first = first;
            this.rest = rest;
        }

        public object First { get { return this.first; } }

        public object Rest { get { return this.rest; } }
    }
}
