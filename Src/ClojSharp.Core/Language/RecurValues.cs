namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class RecurValues
    {
        private IList<object> elements;

        public RecurValues(IList<object> elements)
        {
            this.elements = elements;
        }

        public IList<object> Elements { get { return this.elements; } }
    }
}
