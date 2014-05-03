namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Vector : IMeta
    {
        private IList<object> elements;
        private Map metadata;

        public Vector(IList<object> elements)
            : this(elements, null)
        {
        }

        internal Vector(IList<object> elements, Map metadata)
        {
            this.elements = elements;
            this.metadata = metadata;
        }

        public IList<object> Elements { get { return this.elements; } }

        public Map Metadata
        {
            get { return this.metadata; }
        }
    }
}
