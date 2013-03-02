namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Vector
    {
        private IList<object> elements;

        public Vector(IList<object> elements)
        {
            this.elements = elements;
        }

        public IList<object> Elements { get { return this.elements; } }
    }
}
